using System;
using System.Collections.Generic;
using System.Drawing;

namespace SpaceSim
{
    public class SpaceObject
    {
        public String name { get; set; }
        public double objectRadius { get; set; }
        public Color color { get; set; }
        public double xPos { get; set; }
        public double yPos { get; set; }

        public SpaceObject(String name, double ObjectRadius, Color color)
        {
            this.name = name;
            this.objectRadius = ObjectRadius;
            this.color = color;
        }
        public virtual void Draw()
        {
            Console.WriteLine("\nName: " + name
                + "\nColor: " + color
                + "\nObject Radius: " + objectRadius);
        }
       
    }
    public class Star : SpaceObject
    {
        public Star(String name, int ObjectRadius, Color color) : base(name, ObjectRadius, color)
        {
            xPos = 600;
            yPos = 300;
        }
        public override void Draw()
        {
            Console.Write("Star : ");
            base.Draw();
        }
    }
    public class Planet : SpaceObject
    {
        public double orbitRadius { get; set; }
        public double orbitPeriod { get; set; }
        public int rotationalPeriod { get; set; }


        public Planet(String name, double ObjectRadius, Color color, double orbitRadius, double orbitPeriod) : base(name, ObjectRadius, color)
        {
            this.orbitRadius = orbitRadius;
            this.orbitPeriod = orbitPeriod;
        }
        public override void Draw()
        {
            Console.Write("Planet: ");
            base.Draw();
            Console.Write("\nOrbit Radius");
        }
    }
    public class Moon : Planet
    {
        public Planet orbits { get; set; }
        public Moon(String name, double ObjectRadius, Color color, Planet orbits, double orbitRadius, double orbitPeriod) : base(name, ObjectRadius, color, orbitRadius, orbitPeriod)
        {
            this.orbits = orbits;
        }
        public override void Draw()
        {
            Console.Write("Moon : ");
            base.Draw();
        }
    }

    public class DwarfPlanet : Planet
    {
        public DwarfPlanet(String name, double ObjectRadius, Color color, double orbitRadius, double orbitPeriod) : base(name, ObjectRadius, color, orbitRadius, orbitPeriod) { }
        public override void Draw()
        {
            Console.Write("Dwarf Planet : ");
            base.Draw();
        }

    }

    public class Asteroid : Planet
    {
        public Asteroid(String name, double ObjectRadius, Color color, double orbitRadius, double orbitPeriod) : base(name, ObjectRadius, color, orbitRadius, orbitPeriod) { }
        public override void Draw()
        {
            Console.Write("Asteroid : ");
            base.Draw();
        }
    }

    public class Comet : SpaceObject
    {
        public Comet(String name, double ObjectRadius, Color color, double orbitPeriod) : base(name, ObjectRadius, color) { }
        public override void Draw()
        {
            Console.Write("Comet : ");
            base.Draw();
        }
    }

    public class AsteroidBelt : Asteroid
    {
        public AsteroidBelt(String name, double ObjectRadius, Color color, double orbitRadius, double orbitPeriod) : base(name, ObjectRadius, color, orbitRadius, orbitPeriod) { }
        public override void Draw()
        {
            Console.Write("Comet : ");
            base.Draw();
        }
    }
}
