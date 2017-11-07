using System;
using System.Collections.Generic;
using System.IO;

public class AtomsList
{
    private List<ElementData> atoms;

    public AtomsList(string dataPath)
    {
        atoms = new List<ElementData>();
        LoadData(dataPath);
    }

    public ElementData this[int index]
    {
        get { return atoms.Find( e => e.index == index ); }
    }

    private void LoadData(string path)
    {
        atoms.Add(new ElementData(1, "H", "Hydrogen", "      Hydrogen is the simplest element of all, and the lightest. It is also by far the most common element in the Universe. Over 90 percent of the atoms in the Universe are hydrogen. On Earth, the major location of hydrogen is in water, H2O. There is little free hydrogen on Earth because hydrogen is so light that it is not held by the planet’s gravity. Any hydrogen that forms eventually escapes from the atmosphere into space. Hydrogen is believed to be one of three elements produced in the Big Bang; the others are helium and lithium."));
        atoms.Add(new ElementData(8, "O", "Oxygen", "      Oxygen is the third most abundant element in the universe. It is unstable in our planet’s atmosphere and must be constantly replenished by photosynthesis in green plants. Without life, our atmosphere would contain almost no oxygen. If we discover any other planets with atmospheres rich in oxygen, we will know that life is almost certainly present on these planets. In addition, almost two-thirds of the weight of living things comes from oxygen, mainly because living things contain a lot of water and 88.9 percent of water’s weight comes from oxygen."));
        atoms.Add(new ElementData(6, "C", "Carbon", "      Carbon is the fourth most abundant element in the universe. It is made within stars when they burn helium in nuclear fusion reactions meaning carbon is part of the 'ash' formed by helium burning. As a result, the carbon atoms in your body were all once part of the carbon dioxide fraction of the atmosphere. More than that, about 20% of the weight of living organisms is carbon. Carbon in a coal form is used as a fuel, and diamonds, based on carbon, are used in jewelry and – because they are so hard – in industry for cutting, drilling and polishing."));
        atoms.Add(new ElementData(11, "Na", "Sodium", "      Sodium is our planet’s sixth most abundant element and is mainly found in many minerals. It is a soft, silvery-white metal which is soft enough to be cut with the edge of a coin. The most common compound of sodium is sodium chloride, NaCl, or ordinary table salt. Humans and other animals need sodium to maintain the correct fluid balance in their cells. Historically, sodium has been used as a form of currency, medical treatments, and trade product for thousands of years. Nowadays, sodium compounds are used in nearly every form of industry."));
        atoms.Add(new ElementData(20, "Ca", "Calcium", "      Calcium is the most abundant of the metallic elements in the human body. The average adult body contains about 2 lb of calcium, 99% of which is in the bones and teeth. Calcium not only builds the structures that support our bodies, many of us also live in homes built using structural concrete or cement made with lime. Snails and many shellfish use another calcium compound – calcium carbonate – to build their own homes too – their shells. We mainly get our body's calcium requirements through food. Vitamin D is needed to absorb this calcium."));
    }
}