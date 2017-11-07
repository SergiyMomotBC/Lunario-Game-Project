using System.Collections.Generic;

public class ChemicalData
{
    private List<ElementsData> atoms;

    public ChemicalData()
    {
        atoms = new List<ElementsData>();
        LoadData();
    }

    public ElementsData this[int index]
    {
        get { return atoms.Find((e) => e.index == index); }
    }

    private void LoadData()
    {
        atoms.Add(new ElementsData(1, "H", "Hydrogen", "This is Hydrogen."));
        atoms.Add(new ElementsData(8, "O", "Oxygen", "This is Oxygen"));
        atoms.Add(new ElementsData(6, "C", "Carbon", "This is Carbon"));
        atoms.Add(new ElementsData(11, "Na", "Sodium", "This is Sodium"));     
    }
}