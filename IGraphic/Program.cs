using System;
using System.Collections.Generic;


public interface IGraphic
{
    void Draw(int depth = 0);
}


public class Line : IGraphic
{
    private string _color;
    private int _length;

    public Line(string color, int length)
    {
        _color = color;
        _length = length;
    }

    public void Draw(int depth = 0)
    {
        Console.WriteLine($"{new string(' ', depth * 2)} Линия [{_color}, {_length}px]");
    }
}

public class Rectangle : IGraphic
{
    private string _color;
    private int _width;
    private int _height;

    public Rectangle(string color, int width, int height)
    {
        _color = color;
        _width = width;
        _height = height;
    }

    public void Draw(int depth = 0)
    {
        Console.WriteLine($"{new string(' ', depth * 2)} Прямоугольник [{_color}, {_width}x{_height}]");
    }
}

public class Circle : IGraphic
{
    private string _color;
    private int _radius;

    public Circle(string color, int radius)
    {
        _color = color;
        _radius = radius;
    }

    public void Draw(int depth = 0)
    {
        Console.WriteLine($"{new string(' ', depth * 2)} Круг [{_color}, радиус {_radius}]");
    }
}

// 3. Композит
public class CompositeGraphic : IGraphic
{
    private string _name;
    private List<IGraphic> _children = new List<IGraphic>();

    public CompositeGraphic(string name)
    {
        _name = name;
    }

    public void Add(IGraphic graphic)
    {
        _children.Add(graphic);
    }

    public void Remove(IGraphic graphic)
    {
        _children.Remove(graphic);
    }

    public void Draw(int depth = 0)
    {
        Console.WriteLine($"{new string(' ', depth * 2)} Группа [{_name}]:");

        foreach (var child in _children)
        {
            child.Draw(depth + 1);
        }
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("ГРАФИЧЕСКИЙ РЕДАКТОР (Паттерн Компоновщик)");

        
        var line1 = new Line("Красный", 100);
        var line2 = new Line("Синий", 75);
        var rect1 = new Rectangle("Зеленый", 50, 30);
        var rect2 = new Rectangle("Желтый", 80, 60);
        var circle1 = new Circle("Оранжевый", 25);
        var circle2 = new Circle("Фиолетовый", 40);
        var circle3 = new Circle("Розовый", 15);

        
        var leftGroup = new CompositeGraphic("Левая группа");
        leftGroup.Add(line1);
        leftGroup.Add(rect1);
        leftGroup.Add(circle1);

        
        var rightGroup = new CompositeGraphic("Правая группа");
        rightGroup.Add(line2);
        rightGroup.Add(rect2);
        rightGroup.Add(circle2);

        
        var innerGroup = new CompositeGraphic("Внутренняя группа");
        innerGroup.Add(circle3);
        innerGroup.Add(new Line("Белый", 50));
        innerGroup.Add(new Rectangle("Голубой", 40, 40));

        
        rightGroup.Add(innerGroup);

        
        var rootGroup = new CompositeGraphic("Корневая группа (Весь рисунок)");
        rootGroup.Add(leftGroup);
        rootGroup.Add(rightGroup);

        
        rootGroup.Add(new Circle("Черный", 10));
        rootGroup.Add(new Line("Серый", 200));

        
        Console.WriteLine("Структура рисунка:");
        Console.WriteLine();
        rootGroup.Draw();

        Console.WriteLine();
        Console.ReadKey();
    }
}