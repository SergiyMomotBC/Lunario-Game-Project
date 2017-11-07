﻿public class ElementData
{
    public readonly int index;
    public readonly string acronym;
    public readonly string name;
    public readonly string description;

    public ElementData(int index, string acronym, string name, string description)
    {
        this.index = index;
        this.acronym = acronym;
        this.name = name;
        this.description = description;
    }
}

