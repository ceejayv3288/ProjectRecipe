using ProjectRecipe.Commands;
using ProjectRecipe.Commands.Navigation;
using ProjectRecipe.Constants;
using ProjectRecipe.Converters;
using ProjectRecipe.Models;
using ProjectRecipe.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProjectRecipe.ViewModels
{
    public class RecipeCreateUpdatePageViewModel : BaseViewModel
    {
        private readonly IValidationService validationService;
        private readonly IRecipeService recipeService;

        ByteArrayToImageConverter byteArrayToImageConverter;
        ImageToByteArrayConverter imageToByteArrayConverter;

        public OpenMediaPickerCommand OpenMediaPickerCommand { get; set; }
        public CreateRecipeCommand CreateRecipeCommand { get; set; }
        public DragStartingCommand DragStartingCommand { get; set; }
        public DropOverCommand DropOverCommand { get; set; }
        public AddRecipeStepCommand AddRecipeStepCommand { get; set; }
        public RecipeStepModel dragStep { get; set; }

        public string[] courseTypes { get; } = Enum.GetNames(typeof(CourseTypeEnum));
        CourseTypeEnum _selectedCourse = CourseTypeEnum.None;
        public CourseTypeEnum selectedCourse
        {
            get => _selectedCourse;
            set => SetProperty(ref _selectedCourse, value);
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

        private string _description;
        public string description
        {
            get { return _description; }
            set
            {
                SetProperty(ref _description, value);
            }
        }

        private int? _duration;
        public int? duration
        {
            get { return _duration; }
            set
            {
                SetProperty(ref _duration, value);
            }
        }

        private ImageSource _image;
        public ImageSource image
        {
            get { return _image; }
            set
            {
                SetProperty(ref _image, value);
            }
        }

        private byte[] _imageByte;
        public byte[] imageByte
        {
            get { return _imageByte; }
            set
            {
                SetProperty(ref _imageByte, value);
            }
        }

        private int _stepsCollectionHeight;
        public int stepsCollectionHeight
        {
            get { return _stepsCollectionHeight; }
            set
            {
                SetProperty(ref _stepsCollectionHeight, value);
            }
        }

        private ObservableCollection<RecipeStepModel> _stepsCollection;
        public ObservableCollection<RecipeStepModel> stepsCollection
        {
            get { return _stepsCollection; }
            set
            {
                SetProperty(ref _stepsCollection, value);
            }
        }

        public RecipeCreateUpdatePageViewModel()
        {
            OpenMediaPickerCommand = new OpenMediaPickerCommand(this);
            PopPageCommand = new PopPageCommand(this);
            CreateRecipeCommand = new CreateRecipeCommand(this);
            DragStartingCommand = new DragStartingCommand(this);
            DropOverCommand = new DropOverCommand(this);
            AddRecipeStepCommand = new AddRecipeStepCommand(this);

            //Command sdasda = new Command<RecipeCreateUpdatePageViewModel>(OnItemDragged);

            validationService = DependencyService.Get<IValidationService>();
            recipeService = DependencyService.Get<IRecipeService>();

            byteArrayToImageConverter = new ByteArrayToImageConverter();
            imageToByteArrayConverter = new ImageToByteArrayConverter();

            stepsCollection = new ObservableCollection<RecipeStepModel>();


            stepsCollection.Add(new RecipeStepModel { guid = Guid.NewGuid(), order = 1, description = "lorem ipsum"});
            stepsCollection.Add(new RecipeStepModel { guid = Guid.NewGuid(), order = 2, description = "lorem ipsum" });
            stepsCollection.Add(new RecipeStepModel { guid = Guid.NewGuid(), order = 3, description = "lorem ipsum" });
        }

        public bool ValidateInput()
        {

            return true;
        }

        //private void OnItemDragged(RecipeCreateUpdatePageViewModel item)
        //{

        //}

        public async Task ExecuteOpenMediaPickerCommand()
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Please pick a photo"
            });

            if (result != null)
            {
                var stream = await result?.OpenReadAsync();
                //image = ImageSource.FromStream(() => stream);
                image = ImageSource.FromFile(result.FullPath);

                Image imageToConvert = new Image
                {
                    Source = ImageSource.FromStream(() => stream)
                };
                imageByte = (byte[])imageToByteArrayConverter.Convert(imageToConvert.Source, null, null, null);
            }
        }

        public async void ExecuteCreateRecipeCommand()
        {
            RecipeCreateModel recipeToCreate = new RecipeCreateModel
            {
                name = this.recipeName,
                description = this.description,
                durationInMin = (int)this.duration,
                image = imageByte
            };

            bool isValid = validationService.ValidateCreateRecipe(recipeToCreate);
            if (!isValid)
                return;
            var samp = await recipeService.CreateRecipe(recipeToCreate);
            await Shell.Current.GoToAsync("..");
        }

        public void ExecuteDragOverDeleteCommand()
        {
            if (stepsCollection.Contains(dragStep) && stepsCollection.Count > 1)
            {
                stepsCollection.Remove(dragStep);
                ArrangeNumber();
                //var recipeDeleted = await recipeService.DeleteRecipe(dragStep.id);
                //if (!recipeDeleted.IsSuccessStatusCode)
                //{
                //    await App.Current.MainPage.DisplayAlert("Failed!", "An error has occured.", "Ok");
                //}
            }
        }

        public void ExecuteAddRecipeStepCommand()
        {
            stepsCollection.Add(new RecipeStepModel
            {
                guid = Guid.NewGuid(),
                order = stepsCollection.Count + 1
            });
        }

        public void ArrangeNumber()
        {
            int start = 1;
            foreach (var step in stepsCollection)
            {
                step.order = start;
                start++;
            }
        }
    }
}
