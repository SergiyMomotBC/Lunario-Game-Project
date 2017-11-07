using System.Collections.Generic;

public class CompoundsList
{
    private List<string> compounds;

    public CompoundsList(string path)
    {
        compounds = new List<string>();

        compounds.Add("Spell needed:  H<size=24>2</size>O\nThis spell is known as formula of water.\nWater is the most common liquid on Earth. If water gets very cold(below 0 degrees Celsius), it freezes and becomes ice. If water gets very hot(above 100 degrees Celsius), it boils and becomes steam. The human body is between 60 % and 70 % water and it can only survive for a day or two without water.");
        compounds.Add("Spell needed:  C<size=24>2</size>H<size=24>6</size>O\nThis spell is known as formula of alcohol.\nAlcoholic beverages, typically containing 3 - 40 % ethanol(another name for alcohol) by volume, have been produced and consumed by humans since pre-historic times. It is classed as a depressant, meaning that it slows down vital functions - resulting in slurred speech, unsteady movement, disturbed perceptions and an inability to react quickly.");
        compounds.Add("Spell needed:  NaHCO<size=24>3</size>\nThis spell is known as formula of baking soda.\nBaking soda is a white crystalline powder. The native chemical and physical properties of baking soda account for its wide range of applications, including cleaning, deodorizing and fire extinguishing. It is also used as a leavening agent in making baked goods such as bread or pancakes. As the gas expands during baking, the cell walls expand as well, creating a leavened product.");
    }

    public string this[int index]
    {
        get { return compounds[index]; }
    }
}

