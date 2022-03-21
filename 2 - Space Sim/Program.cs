using System;
using System.Collections.Generic;
using SpaceSim;


public class Astronomy
{
    public static List<SpaceObject> body = new List<SpaceObject>();
    public static List<Moon> moons = new List<Moon>(); //Liste av måner

    public static void Main()
    {
        Console.WriteLine("How many days have gone?");
        int days = int.Parse(Console.ReadLine());

        Console.WriteLine("What planet would you like to focus on? Blank answer if all");
        var infoPlanet = Console.ReadLine();

        bool funnet = false;
        foreach (SpaceObject obj in body) //Printer ut spesifikt om én planet og deres måner
        {
            if (String.Equals(obj.name, infoPlanet)) {
                CalcPlanetPos((Planet) obj, days, body[0]);
                foreach (Moon moon in moons)
                {
                    if (moon.orbits.name.Equals(obj.name)) //Beregner månen(e) posisjon
                    {
                        CalcMoonPos((Planet) obj, days, moon);
                    }
                }
                funnet = true;
                break;
            }
        }
        
        if (!funnet)
        {
            CalcAllPlanetPos(body, days);
        }
    }

    //Kalkulerer alle planetenes posisjon i solsystemet
    public static List<SpaceObject> CalcAllPlanetPos (List<SpaceObject> list, int days)
    {
        foreach (SpaceObject obj in list)
        {
            if (obj is Planet && !(obj is DwarfPlanet) && !(obj is Moon) && !(obj is Asteroid))
            {
                Planet planet = (Planet)obj;

                //Solens posisjon + planetenes posisjon i bane
                planet.yPos = list[0].yPos + (Math.Sin(days / planet.orbitPeriod) * 360) * planet.orbitRadius / 20;
                planet.xPos = list[0].xPos + (Math.Cos(days / planet.orbitPeriod) * 360) * planet.orbitRadius / 20;

                //drawPlanet(planet);
            }

           if (obj.name.Equals("Sun"))
            {
                Star sun = (Star) obj;

               // drawPlanet(sun);
            }
        }
        return list;
    }

    //Kalkulerer planetens posisjon i solsystemet
    public static SpaceObject CalcPlanetPos(Planet planet, int days, SpaceObject sun)
    {
        //Solens posisjon + planetens posisjon i bane
        planet.yPos = sun.yPos + (Math.Sin(days / planet.orbitPeriod) * 360) * planet.orbitRadius;
        planet.xPos = sun.xPos + (Math.Cos(days / planet.orbitPeriod) * 360) * planet.orbitRadius;

        drawPlanet(planet);

        return planet;
    }

    public static SpaceObject CalcMoonPos (Planet orbits, int days, Moon moon)
    {
        moon.yPos = orbits.yPos + (Math.Sin(days / moon.orbitPeriod) * 360) * moon.orbitRadius;
        moon.xPos = orbits.xPos + (Math.Cos(days / moon.orbitPeriod) * 360) * moon.orbitRadius;

        drawPlanet(moon);

        return moon;
    }

    public static List<Moon> CalcMoonsPos(int days, List<Moon> moons)
    {

        foreach (Moon moon in moons)
        {
            moon.yPos = moon.orbits.yPos + (Math.Sin(days / moon.orbitPeriod) * 360) * moon.orbitRadius;
            moon.xPos = moon.orbits.xPos + (Math.Cos(days / moon.orbitPeriod) * 360) * moon.orbitRadius;
        }

        return moons;
    }

    public static void drawPlanet (SpaceObject planet)
    {
        Console.WriteLine(planet.name + " is in y posision: " + planet.yPos);
        Console.WriteLine(planet.name + " is in x position: " + planet.xPos);
        Console.WriteLine();
    }

    private static int Scale(int value)
    {

        double scaled = ((value - 5 / 2) * (130 - 5) / ((1392 / 2) - 5 / 2)) + 5;
        return (int)scaled;
    }

    public static List<Moon> getMoonList()
    {
        foreach (SpaceObject obj in body)
        {
            if (obj is Moon)
            {
                moons.Add((Moon)obj);
            }
        }

        return moons;
    }

    public static List<SpaceObject> getList()
    {

        body.Add(new Star("Sun", Scale(1392/2), System.Drawing.Color.Yellow));

        body.Add(new Planet("Mercury", Scale(5/2), System.Drawing.Color.Gray, 57910, 87.97));
        body.Add(new Planet("Venus", Scale(120 / 2), System.Drawing.Color.Yellow, 78200, 224.7));
        body.Add(new Planet("Terra", Scale(129 / 2), System.Drawing.Color.Blue, 99600, 365.26));
        body.Add(new Planet("Mars", Scale(70 / 2), System.Drawing.Color.Red, 127940, 686.98));
        body.Add(new Planet("Jupiter", Scale(1020 / 2), System.Drawing.Color.Brown, 308330, 4332.71));
        body.Add(new Planet("Saturn", Scale(905 / 2), System.Drawing.Color.Orange, 429400, 10759.5));
        body.Add(new Planet("Uranus", Scale(511 / 2), System.Drawing.Color.Blue, 529400, 30685));
        body.Add(new Planet("Neptune", Scale(495 / 2), System.Drawing.Color.Blue, 629400, 60190));

        body.Add(new DwarfPlanet("Pluto", Scale(20 / 2), System.Drawing.Color.Gray, 5913520, 90550));

        body.Add(new Moon("The Moon", Scale(20 / 2), System.Drawing.Color.Gray, (Planet)body.Find(planet => planet.name.Equals("Terra")), 1, 27.32));

        body.Add(new Moon("Phobos", Scale(15 / 2), System.Drawing.Color.Gray, (Planet)body.Find(planet => planet.name.Equals("Mars")), 1, 1.32));
        body.Add(new Moon("Deimos", Scale(15 / 2), System.Drawing.Color.Gray, (Planet)body.Find(planet => planet.name.Equals("Mars")), 2, 1.26));

        body.Add(new Moon("Io", Scale(15 / 2), System.Drawing.Color.Yellow, (Planet)body.Find(planet => planet.name.Equals("Jupiter")), 1, 1.77));
        body.Add(new Moon("Europa", Scale(15 / 2), System.Drawing.Color.White, (Planet)body.Find(planet => planet.name.Equals("Jupiter")), 2, 3.55));
        body.Add(new Moon("Ganymede", Scale(15 / 2), System.Drawing.Color.Brown, (Planet)body.Find(planet => planet.name.Equals("Jupiter")), 3, 7.15));

        /*
        body.Add(new AsteroidBelt("Asteroid Belt closest", Scale(15 / 2), System.Drawing.Color.Gray, 329115, 3457));
        body.Add(new AsteroidBelt("Asteroid Belt farthest", Scale(15 / 2), System.Drawing.Color.Gray, 478713, 4332.71));

        body.Add(new Asteroid("Apophis", 1 / 2, System.Drawing.Color.Gray, 170000, 34357));
        */
        return body;
    }

    }
