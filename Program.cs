namespace Aquarium;
class Program
{
    static void Main(string[] args)
    {
        FishLife simulator = new FishLife();
        
        simulator.SimulateLife();
    }
}

class Fish
{
    private int _health;
    private int _age;
    
    public Fish(string name)
    {
        _health = 10;
        _age = 0;
        Name = name;
    }
    
    public string Name { get; }

    public void GetOlder()
    {
        if (_health == 0)
            return;
        
        _age++;
        _health--;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"Рыбка: {Name}. Здоровье: {_health}. Возраст: {_age} месяцев.");
    }
}

class Pool
{
    private List<Fish> _allFish;
    private int _capacity = 5;

    public Pool()
    {
        _allFish = new List<Fish>();
    }

    public int FishCount => _allFish.Count;

    public void Add(Fish fish)
    {
        if (_allFish.Count == _capacity)
        {
            Console.WriteLine("В аквариуме максимальное кол-во рыб.");
            return;
        }
        
        _allFish.Add(fish);
        Console.WriteLine($"Вы выпустили рыбу {fish.Name} в аквариум.");
    }

    public void Remove(Fish fish)
    {
        if (_allFish.Contains(fish) == false)
        {
            Console.WriteLine("Такой рыбы в аквариуме нет.");
            return;
        }

        _allFish.Remove(fish);
        Console.WriteLine($"Вы вытащили рыбу {fish.Name} из аквариума.");
    }

    public Fish GetFishByIndex(int index)
    {
         return _allFish[index];
    }

    public void ShowInfo()
    {
        Console.WriteLine($"В аквариуме плавает рыб: {_allFish.Count}.\n" +
                          $"Есть место ещё для {_capacity - _allFish.Count}\n");
             
        for (int i = 0; i < _allFish.Count; i++)
        {
            Console.Write(i + 1 + ". ");
            _allFish[i].ShowInfo();
        }
    }
}

class FishLife
{
    private Pool _aquarium;

    public FishLife()
    {
        _aquarium = new Pool();
    }

    public void SimulateLife()
    {
        bool isWorking = true;

        while (isWorking)
        {
            const char ShowStatsCommand = '1';
            const char AddFishCommand = '2';
            const char RemoveFishCommand = '3';
            const char LiveCommand = '4';
            const char ExitCommand = '5';
            
            Console.Clear();
            Console.WriteLine("Аквариум v0.0001\n\n" +
                              $"{ShowStatsCommand} - показать информацию.\n" +
                              $"{AddFishCommand} - выпустить рыбку.\n" +
                              $"{RemoveFishCommand} - вытащить рыбку.\n" +
                              $"{LiveCommand} - прожить один месяц.\n" +
                              $"{ExitCommand} - выйти.\n\n" +
                              "Введите команду:");

            switch (Console.ReadKey(true).KeyChar)
            {
                case ShowStatsCommand:
                    _aquarium.ShowInfo();
                    break;
                
                case AddFishCommand:
                    AddFish();
                    break;
                
                case RemoveFishCommand:
                    RemoveFish();
                    break;
                
                case LiveCommand:
                    Live();
                    break;
                
                case ExitCommand:
                    isWorking = false;
                    break;
            }
            
            if (isWorking == false)
                continue;
            
            Console.WriteLine("\n\nДля продолжения нажмите любую кнопку...");
            Console.ReadKey();
        }
    }

    private void AddFish()
    {
        string name = String.Empty;
        
        while (name == String.Empty)
        {
            Console.WriteLine("Дайте рыбке имя: ");
            name = Console.ReadLine().Trim();
            
            if (name == String.Empty)
                Console.Write("Имя не может быть пустым! ");
        }
        
        _aquarium.Add(new Fish(name));
    }

    private void RemoveFish()
    {
        int fishNumber;
        Fish fish;
        
        if (_aquarium.FishCount == 0)
        {
            Console.WriteLine("В аквариуме нет рыб.");
            return;
        }
        
        _aquarium.ShowInfo();
        
        Console.Write("Чтобы поймать рыбку, введите её номер: ");
        
        while (int.TryParse(Console.ReadLine(), out fishNumber) == false ||
               fishNumber <= 0 ||
               fishNumber > _aquarium.FishCount)
            Console.WriteLine($"Номер должен быть положительным числом не больше {_aquarium.FishCount}: ");
        
        fish = _aquarium.GetFishByIndex(fishNumber - 1);
        _aquarium.Remove(fish);
    }

    private void Live()
    {
        for (int i = 0; i < _aquarium.FishCount; i++)
            _aquarium.GetFishByIndex(i).GetOlder();
        
        Console.WriteLine("Прошёл месяц...");
    }
}