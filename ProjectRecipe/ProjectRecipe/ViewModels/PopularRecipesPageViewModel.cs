using ProjectRecipe.Commands;
using ProjectRecipe.Constants;
using ProjectRecipe.Converters;
using ProjectRecipe.Models;
using ProjectRecipe.Services.Interfaces;
using ProjectRecipe.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace ProjectRecipe.ViewModels
{
    public class PopularRecipesPageViewModel : BaseViewModel
    {
        ByteArrayToImageConverter byteArrayToImageConverter;
        ImageToByteArrayConverter imageToByteArrayConverter;

        private readonly IRecipeService recipeService;
        public Command ChangeSelectedRecipeCommand { get; set; }
        public Command ChangeSelectedCourseTypeCommand { get; set; }
        public Command SelectRecipeCommand { get; set; }

        private ObservableCollection<RecipeModel> _popularRecipesCollection;
        public ObservableCollection<RecipeModel> popularRecipesCollection
        {
            get { return _popularRecipesCollection; }
            set
            {
                SetProperty(ref _popularRecipesCollection, value);
            }
        }

          private List<RecipeModel> _allPopularRecipesList;
        public List<RecipeModel> allPopularRecipesList
        {
            get { return _allPopularRecipesList; }
            set
            {
                SetProperty(ref _allPopularRecipesList, value);
            }
        }

        private RecipeModel _selectedRecipe;
        public RecipeModel selectedRecipe
        {
            get { return _selectedRecipe; }
            set
            {
                SetProperty(ref _selectedRecipe, value);
            }
        }

        private string _recipeDuration;
        public string recipeDuration
        {
            get { return _recipeDuration; }
            set
            {
                SetProperty(ref _recipeDuration, value);
            }
        }

        private int _recipeLikesCount;
        public int recipeLikesCount
        {
            get { return _recipeLikesCount; }
            set
            {
                SetProperty(ref _recipeLikesCount, value);
            }
        }

        private int _recipeCommentsCount;
        public int recipeCommentsCount
        {
            get { return _recipeCommentsCount; }
            set
            {
                SetProperty(ref _recipeCommentsCount, value);
            }
        }

        private string _recipeDescription;
        public string recipeDescription
        {
            get { return _recipeDescription; }
            set
            {
                SetProperty(ref _recipeDescription, value);
            }
        }

        private string _recipeName;
        public string recipeName
        {
            get { return _recipeName; }
            set
            {
                SetProperty(ref _recipeName, value);
            }
        }

        private string _selectedCourseType;
        public string selectedCourseType
        {
            get { return _selectedCourseType; }
            set
            {
                SetProperty(ref _selectedCourseType, value);
            }
        }

        public PopularRecipesPageViewModel()
        {
            byteArrayToImageConverter = new ByteArrayToImageConverter();
            imageToByteArrayConverter = new ImageToByteArrayConverter();

            OpenFlyoutMenuCommand = new OpenFlyoutMenuCommand(this);
            ChangeSelectedRecipeCommand = new Command(ExecuteChangeSelectedRecipeCommand);
            ChangeSelectedCourseTypeCommand = new Command<string>(ExecuteChangeSelectedCourseTypeCommand);
            SelectRecipeCommand = new Command<object>(ExecuteSelectRecipeCommand);
            popularRecipesCollection = new ObservableCollection<RecipeModel>();
            allPopularRecipesList = new List<RecipeModel>();
            selectedCourseType = "2";

            recipeService = DependencyService.Get<IRecipeService>();

            PopulateList();
        }

        private async void ExecuteSelectRecipeCommand(object obj)
        {
            isBusy = true;
            try
            {
                if (obj is RecipeModel recipe)
                {
                    var route = $"{nameof(RecipeDetailsPage)}?recipeId={recipe.id}";
                    await Shell.Current.GoToAsync(route);
                }
            }
            finally
            {
                isBusy = false;
            }
        }

        public void ExecuteChangeSelectedCourseTypeCommand(string courseType)
        {
            var ct = Enum.Parse(typeof(CourseTypeEnum), courseType);
            switch (ct)
            {
                case CourseTypeEnum.Appetizer:
                    selectedCourseType = "1";
                    popularRecipesCollection.Clear();
                    allPopularRecipesList.FindAll(x => x.courseType == CourseTypeEnum.Appetizer).ForEach(x => popularRecipesCollection.Add(x));
                    break;
                case CourseTypeEnum.Entrees:
                    selectedCourseType = "2";
                    popularRecipesCollection.Clear();
                    allPopularRecipesList.FindAll(x => x.courseType == CourseTypeEnum.Entrees).ForEach(x => popularRecipesCollection.Add(x));
                    break;
                case CourseTypeEnum.Dessert:
                    selectedCourseType = "3";
                    popularRecipesCollection.Clear();
                    allPopularRecipesList.FindAll(x => x.courseType == CourseTypeEnum.Dessert).ForEach(x => popularRecipesCollection.Add(x));
                    break;
                default:
                    selectedCourseType = "2";
                    popularRecipesCollection.Clear();
                    allPopularRecipesList.FindAll(x => x.courseType == CourseTypeEnum.Entrees).ForEach(x => popularRecipesCollection.Add(x));
                    break;
            }
        }

        public void ExecuteChangeSelectedRecipeCommand()
        {
            if (selectedRecipe != null)
            {
                recipeDescription = selectedRecipe.description;
                recipeCommentsCount = selectedRecipe.commentsCount;
                recipeDuration = selectedRecipe.durationInMin.ToString();
                recipeLikesCount = selectedRecipe.likesCount;
                recipeName = selectedRecipe.name;
            }
        }

        public async void PopulateList()
        {
            allPopularRecipesList = await recipeService.GetPopularRecipes();
            allPopularRecipesList?.ForEach(x => popularRecipesCollection.Add(x));

            //Image embeddedImage = new Image
            //{
            //    Source = ImageSource.FromResource("ProjectRecipe.Assets.Images.recipe-placeholder.png", typeof(ImageResourceExtension).GetTypeInfo().Assembly)
            //};
            //var sampleByte = imageToByteArrayConverter.Convert(embeddedImage.Source, null, null, null);

            //byte[] fileByte = new byte[] { };
            ////ImageSource convertedImage = byteArrayToImageConverter.Convert(fileByte, null, null, null) as ImageSource;

            //ImageSource convertedImage = byteArrayToImageConverter.Convert(sampleByte, null, null, null) as ImageSource;

            //popularRecipesList.Add(new RecipeModel { name = "test", description = "test", image = convertedImage });
            //popularRecipesList.Add(new RecipeModel { name = "test2", description = "test2", image = convertedImage });

            ////PopularRecipesList.Add(new RecipeModel { Name = "test", Description = "test", Image = embeddedImage.Source });
            ////PopularRecipesList.Add(new RecipeModel { Name = "test2", Description = "test2", Image = embeddedImage.Source });
        }
    }
}
