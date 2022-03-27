using ProjectRecipe.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

[assembly: ExportFont("Raleway-Regular.ttf", Alias = "RalewayRegular")]
[assembly: ExportFont("Raleway-Bold.ttf", Alias = "RalewayBold")]
[assembly: ExportFont("Raleway-ExtraBold.ttf", Alias = "RalewayExtraBold")]
[assembly: ExportFont("Raleway-Italic.ttf", Alias = "RalewayItalic")]

[assembly: Dependency(typeof(RecipeService))]
[assembly: Dependency(typeof(RecipeStepService))]
[assembly: Dependency(typeof(RecipeIngredientService))]
[assembly: Dependency(typeof(ValidationService))]
[assembly: Dependency(typeof(LikeService))]
[assembly: Dependency(typeof(AuthorizationService))]
[assembly: Dependency(typeof(ErrorService))]
[assembly: Dependency(typeof(MemoryCacheService))]