using ProjectRecipe.Commands;
using ProjectRecipe.Commands.Draggable;
using ProjectRecipe.Commands.Navigation;
using ProjectRecipe.Constants;
using ProjectRecipe.Converters;
using ProjectRecipe.Models;
using ProjectRecipe.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        public ItemDraggedCommand ItemDraggedCommand { get; set; }
        public ItemDroppedCommand ItemDroppedCommand { get; set; }
        public ItemDraggedOverCommand ItemDraggedOverCommand { get; set; }
        public ItemDraggedLeaveCommand ItemDraggedLeaveCommand { get; set; }
        public AddRecipeStepCommand AddRecipeStepCommand { get; set; }

        public int StepDefaultHeight { get; set; } = 52;

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
            ItemDraggedCommand = new ItemDraggedCommand(this);
            ItemDroppedCommand = new ItemDroppedCommand(this);
            ItemDraggedOverCommand = new ItemDraggedOverCommand(this);
            ItemDraggedLeaveCommand = new ItemDraggedLeaveCommand(this);
            AddRecipeStepCommand = new AddRecipeStepCommand(this);
            validationService = DependencyService.Get<IValidationService>();
            recipeService = DependencyService.Get<IRecipeService>();

            byteArrayToImageConverter = new ByteArrayToImageConverter();
            imageToByteArrayConverter = new ImageToByteArrayConverter();

            stepsCollection = new ObservableCollection<RecipeStepModel>();

            stepsCollection.Add(new RecipeStepModel { guid = Guid.NewGuid(), order = 1, description = "" });
            stepsCollectionHeight += StepDefaultHeight;
        }

        public bool ValidateInput()
        {

            return true;
        }

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
            RecipeCreateUpdateModel recipeToCreate = new RecipeCreateUpdateModel
            {
                name = this.recipeName,
                description = this.description,
                durationInMin = (int)this.duration,
                image = imageByte,
                courseType = (int)selectedCourse
            };

            bool isValidRecipe = validationService.ValidateCreateRecipe(recipeToCreate);
            if (!isValidRecipe)
                return;
            bool isValidRecipeStep = validationService.ValidateCreateRecipeStep(stepsCollection.ToList());
            if (!isValidRecipeStep)
                return;

            var isRecipeCreated = await recipeService.CreateRecipe(recipeToCreate);
            var isRecipeStepsCreated = await recipeService.CreateRecipe(recipeToCreate);
            if (isRecipeCreated.IsSuccessStatusCode)
                await Shell.Current.GoToAsync("..");
            else
                await App.Current.MainPage.DisplayAlert("Failed!", "An error has occured.", "Ok");
        }

        public void ExecuteItemDraggedCommand(RecipeStepModel recipeStep)
        {
            stepsCollection.ToList().ForEach(i => i.isBeingDragged = recipeStep == i);
        }

        public void ExecuteItemDroppedMoveRecipeStepCommand(RecipeStepModel recipeStep)
        {
            var itemToMove = stepsCollection.First(i => i.isBeingDragged);
            var itemToInsertBefore = recipeStep;

            if (itemToMove == null || itemToInsertBefore == null || itemToMove == itemToInsertBefore)
                return;

            stepsCollection.Remove(itemToMove);
            var insertAtIndex = stepsCollection.IndexOf(itemToInsertBefore);

            stepsCollection.Insert(insertAtIndex, itemToMove);
            itemToMove.isBeingDragged = false;
            itemToInsertBefore.isBeingDraggedOver = false;
            stepsCollectionHeight -= StepDefaultHeight;

            ArrangeNumbering();
        }

        public async void ExecuteItemDroppedDeleteRecipeStepCommand()
        {
            if (stepsCollection.Count > 1)
            {
                var itemToDelete = stepsCollection.First(i => i.isBeingDragged);
                stepsCollection.Remove(itemToDelete);
                stepsCollectionHeight -= StepDefaultHeight;
                ArrangeNumbering();
                var recipeDeleted = await recipeService.DeleteRecipe(itemToDelete.id);
                if (!recipeDeleted.IsSuccessStatusCode)
                {
                    await App.Current.MainPage.DisplayAlert("Failed!", "An error has occured.", "Ok");
                }
            }
        }

        public void ExecuteItemDraggedOverRecipeStepCommand(RecipeStepModel recipeStep)
        {
            var itemBeingDragged = stepsCollection.FirstOrDefault(i => i.isBeingDragged);
            stepsCollection.ToList().ForEach(i => i.isBeingDraggedOver = recipeStep == i && recipeStep != itemBeingDragged);
            stepsCollectionHeight = (stepsCollection.Count() + 1) * StepDefaultHeight;
        }

        public void ExecuteItemDraggedLeaveRecipeStepCommand(RecipeStepModel recipeStep)
        {
            stepsCollection.ToList().ForEach(i => i.isBeingDraggedOver = false);
            stepsCollectionHeight = stepsCollection.Count() * StepDefaultHeight;
        }

        public void ExecuteAddRecipeStepCommand()
        {
            stepsCollection.Add(new RecipeStepModel
            {
                guid = Guid.NewGuid(),
                order = stepsCollection.Count + 1
            });
            stepsCollectionHeight += StepDefaultHeight;
        }

        public void ArrangeNumbering()
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
