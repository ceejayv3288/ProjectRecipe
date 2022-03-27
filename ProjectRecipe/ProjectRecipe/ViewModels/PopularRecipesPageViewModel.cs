using ProjectRecipe.Commands;
using ProjectRecipe.Commands.Navigation;
using ProjectRecipe.Constants;
using ProjectRecipe.Converters;
using ProjectRecipe.Models;
using ProjectRecipe.Services.Interfaces;
using ProjectRecipe.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private readonly ILikeService likeService;

        public Command ChangeSelectedRecipeCommand { get; set; }
        public Command ChangeSelectedCourseTypeCommand { get; set; }
        public Command SelectRecipeCommand { get; set; }
        public PageAppearingCommand PageAppearingCommand { get; set; }
        public RecipeDetailsNavigationPageCommand RecipeDetailsNavigationPageCommand { get; set; }
        public LikeRecipeCommand LikeRecipeCommand { get; set; }

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
            PageAppearingCommand = new PageAppearingCommand(this);
            RecipeDetailsNavigationPageCommand = new RecipeDetailsNavigationPageCommand(this);
            LikeRecipeCommand = new LikeRecipeCommand(this);
            popularRecipesCollection = new ObservableCollection<RecipeModel>();
            allPopularRecipesList = new List<RecipeModel>();
            selectedCourseType = "2";

            recipeService = DependencyService.Get<IRecipeService>();
            likeService = DependencyService.Get<ILikeService>();
        }

        public void ExecutePageAppearingCommand()
        {
            PopulateList();
        }

        public async void ExecuteLikeRecipeCommand(int recipeId)
        {
            var likeResult = await likeService.LikeUnlikeRecipe(recipeId, App.UserId);
            var likeRecipe = allPopularRecipesList?.FirstOrDefault(x => x.id == likeResult.RecipeId);
            if (likeRecipe != null)
            {
                likeRecipe.isLiked = likeResult.IsLiked;
                likeRecipe.likesCount = likeRecipe.isLiked ? likeRecipe.likesCount + 1 : likeRecipe.likesCount - 1;
            }
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

        public async void ExecuteRecipeDetailsNavigationPageCommand(int recipeId)
        {
            isBusy = true;
            try
            {
                var route = $"{nameof(RecipeDetailsPage)}?recipeId={recipeId}";
                await Shell.Current.GoToAsync(route);
            }
            finally
            {
                isBusy = false;
            }
        }

        public void ExecuteChangeSelectedCourseTypeCommand(string courseType)
        {
            var ct = Enum.Parse(typeof(CourseTypeEnum), courseType);
            var recipesToRemove = popularRecipesCollection.ToList();
            selectedRecipe = popularRecipesCollection.FirstOrDefault(); //to avoid crashing before changing the collection
            switch (ct)
            {
                case CourseTypeEnum.Appetizer:
                    selectedCourseType = "1";
                    allPopularRecipesList.FindAll(x => x.courseType == CourseTypeEnum.Appetizer).ForEach(x => popularRecipesCollection.Add(x));
                    break;
                case CourseTypeEnum.Entrees:
                    selectedCourseType = "2";
                    allPopularRecipesList.FindAll(x => x.courseType == CourseTypeEnum.Entrees).ForEach(x => popularRecipesCollection.Add(x));
                    break;
                case CourseTypeEnum.Dessert:
                    selectedCourseType = "3";
                    allPopularRecipesList.FindAll(x => x.courseType == CourseTypeEnum.Dessert).ForEach(x => popularRecipesCollection.Add(x));
                    break;
                default:
                    selectedCourseType = "2";
                    allPopularRecipesList.FindAll(x => x.courseType == CourseTypeEnum.Entrees).ForEach(x => popularRecipesCollection.Add(x));
                    break;
            }
            recipesToRemove.ForEach(x => popularRecipesCollection.Remove(x));
        }

        public void ExecuteChangeSelectedRecipeCommand()
        {
        }

        public async void PopulateList()
        {
            allPopularRecipesList.Clear();
            allPopularRecipesList = await recipeService.GetPopularRecipes();
            allPopularRecipesList?.ForEach(x => popularRecipesCollection.Add(x));

            ExecuteChangeSelectedCourseTypeCommand(selectedCourseType);

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
