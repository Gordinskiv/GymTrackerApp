using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PracticaGymTracker.Models;

namespace PracticaGymTracker.ViewModels;

/// <summary>
/// ViewModel для сторінки каталогу вправ.
/// Відповідає за відображення списку доступних тренувань, згрупованих за категоріями.
/// </summary>
public partial class ExercisesCatalogViewModel : ViewModelBase
{
    /// <summary>
    /// Колекція, що містить усі групи вправ для відображення в меню.
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<MuscleGroupItem> _catalog;

    /// <summary>
    /// Назва обраної вправи для відображення на головному екрані каталогу.
    /// </summary>
    private ExerciseItem _selectedExercise;

    public ExerciseItem SelectedExercise
    {
        get => _selectedExercise;
        set
        {
            if (value != null)
            {
                SetProperty(ref _selectedExercise, value);
            }
        }
    }

    public ExercisesCatalogViewModel()
    {
        Catalog = new ObservableCollection<MuscleGroupItem>
        {
            new MuscleGroupItem
            {
                GroupName = "ГРУДНІ М'ЯЗИ",
                Exercises = new ObservableCollection<ExerciseItem>
                {
                    new ExerciseItem { Name = "Жим лежачи", TargetMuscle = "Chest", SecondaryTargetMuscle = "Triceps", 
                        Description = "Базова багатосуглобова вправа для розвитку грудей. Ляжте на лаву, міцно притисніть лопатки та сідниці. Візьміться за гриф закритим хватом трохи ширше плечей. Повільно опустіть штангу до середини грудей, зробіть невелику паузу і потужно витисніть її вгору на видиху.",
                        ImagePath = "avares://GymTrackerApp/Assets/BenchPress.jpeg"
                    },
                    new ExerciseItem
                    {
                        Name = "Жим на похилій лаві", 
                        TargetMuscle = "Chest", 
                        SecondaryTargetMuscle = "Triceps",
                        Description = "Акцентує навантаження на верхню частину грудних м'язів. Встановіть лаву під кутом 30-45 градусів. Опускайте штангу (або гантелі) ближче до ключиць. Ця вправа допомагає створити об'ємний і гармонійний вигляд грудей.",
                        ImagePath = "avares://GymTrackerApp/Assets/BenchPress45.jpeg"
                    },
                    new ExerciseItem { Name = "Віджимання на брусах", 
                        TargetMuscle = "Chest", 
                        SecondaryTargetMuscle = "Triceps",
                        Description = "Відмінна базова вправа з власною вагою для нижньої частини грудей та трицепсів. Нахиліть корпус трохи вперед, щоб змістити акцент саме на грудні м'язи. Опускайтеся до кута 90 градусів у ліктях, після чого потужно витисніть себе вгору.",
                        ImagePath = "avares://GymTrackerApp/Assets/Dips.jpeg" },
                }
            },
            new MuscleGroupItem
            {
                GroupName = "СПИНА",
                Exercises = new ObservableCollection<ExerciseItem>
                {
                    new ExerciseItem { Name = "Вертикальна тяга", 
                        TargetMuscle = "Back",
                        SecondaryTargetMuscle = "Biceps",
                        Description = "Чудова вправа для формування V-подібної фігури (ширини спини). Сядьте в тренажер, зафіксуйте ноги валиками. Візьміться за ручку широким прямим хватом. На видиху тягніть ручку до верхньої частини грудей, максимально зводячи лопатки разом.",
                        ImagePath = "avares://GymTrackerApp/Assets/VerticalThrust.jpeg" },
                    new ExerciseItem { Name = "Горизонтальна тяга", 
                        TargetMuscle = "Back",
                        SecondaryTargetMuscle = "Biceps",
                        Description = "Розвиває товщину спини. Сядьте рівно, злегка зігніть коліна. Тримаючи спину прямою, тягніть рукоятку до пояса, зводячи лопатки разом. Не розгойдуйте корпус — рух має відбуватися лише за рахунок м'язів спини.",
                        ImagePath = "avares://GymTrackerApp/Assets/SeatedRow.jpeg" },
                    new ExerciseItem { Name = "Підтягування", 
                        TargetMuscle = "Back",
                        SecondaryTargetMuscle = "Biceps",
                        Description = "Найкраща базова вправа з власною вагою для найширших м'язів. Візьміться за турнік прямим хватом трохи ширше плечей. Тягніться підборіддям вище перекладини, фокусуючись на роботі м'язів спини, а не рук.",
                        ImagePath = "avares://GymTrackerApp/Assets/Pullups.jpeg" }
                }
            },
            new MuscleGroupItem
            {
                GroupName = "НОГИ",
                Exercises = new ObservableCollection<ExerciseItem>
                {
                    new ExerciseItem { Name = "Присід зі штангою", 
                        TargetMuscle = "Legs",
                        SecondaryTargetMuscle = "Glutes",
                        Description = "Головна базова вправа для побудови сильних ніг. Розмістіть штангу на верхній частині спини (на трапеціях). Відводячи таз назад, плавно присядьте до паралелі стегон з підлогою. Спину тримайте рівною, не зводьте коліна всередину.",
                        ImagePath = "avares://GymTrackerApp/Assets/Squat.jpeg" },
                    new ExerciseItem { Name = "Випади", 
                        TargetMuscle = "Legs",
                        SecondaryTargetMuscle = "Glutes",
                        Description = "Ізольована вправа для опрацювання квадрицепсів та сідниць. Зробіть широкий крок вперед, тримаючи корпус рівно. Опускайтеся вниз, поки коліно задньої ноги майже не торкнеться підлоги. Відштовхніться передньою ногою, щоб повернутися у вихідне положення.",
                        ImagePath = "avares://GymTrackerApp/Assets/Lunges.jpeg" },
                    new ExerciseItem { Name = "Жим ногами", 
                        TargetMuscle = "Legs",
                        SecondaryTargetMuscle = "Calves",
                        Description = "Безпечна альтернатива присіданням для акцентованого навантаження на ноги без навантаження на поперек. Розмістіть стопи на платформі. Плавно опустіть вагу до кута 90 градусів у колінах, потім потужно витисніть платформу вгору, не випрямляючи коліна до кінця.",
                        ImagePath = "avares://GymTrackerApp/Assets/LegPress.jpeg" },
                    new ExerciseItem { Name = "Піднімання на носки", 
                        TargetMuscle = "Legs",
                        SecondaryTargetMuscle = "Calves",
                        Description = "Ізольована вправа для литкових м'язів. Станьте носками на платформу, п'яти звисають. Плавно опустіть п'яти максимально вниз для розтягнення, потім потужно підніміться на носки, затримуючись у верхній точці на секунду.",
                        ImagePath = "avares://GymTrackerApp/Assets/CalfRaises.jpeg" },
                    new ExerciseItem 
                    { 
                        Name = "Станова тяга", 
                        TargetMuscle = "Back extensor muscles", 
                        SecondaryTargetMuscle = "Buttocks, Hamstrings, Trapezius", 
                        Description = "Найважливіша силова вправа для всього тіла. Підійдіть впритул до штанги. Присядьте з прямою спиною і візьміться за гриф. Напружте м'язи кору і, відштовхуючись ногами від підлоги, підніміть штангу, повністю випрямляючись у верхній точці. Важливо тримати спину ідеально рівною протягом усього руху.", 
                        ImagePath = "avares://GymTrackerApp/Assets/Deadlift.jpeg"
                    },
                    new ExerciseItem 
                    { 
                    Name = "Болгарські спліт-присідання", 
                    TargetMuscle = "Quadriceps, Glutes", 
                    SecondaryTargetMuscle = "Cortical muscles (balance)", 
                    Description = "Ефективна ізольована вправа для кожної ноги окремо. Станьте спиною до лави, покладіть на неї підйом однієї стопи. Зробіть крок вперед опорною ногою. Плавно опускайтеся вниз, поки стегно передньої ноги не стане паралельним підлозі. Зберігайте вертикальне положення корпусу.", 
                    ImagePath = "avares://GymTrackerApp/Assets/Bulgarian.jpeg" 
                    }
                }
            }
        };
        if (Catalog.Count > 0 && Catalog[0].Exercises.Count > 0)
        {
            SelectedExercise = Catalog[0].Exercises[0];
        }
    }
}