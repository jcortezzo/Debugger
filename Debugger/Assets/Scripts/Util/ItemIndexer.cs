using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIndexer : MonoBehaviour
{
    public static ItemIndexer Instance;
    public const int BASE_ITEMS = 5;
    private const string RESOURCES_PATH = "Prefabs/Weapons/";
    private Dictionary<int, string> WeaponIndex = new Dictionary<int, string>
    {
        {-1 , "Empty" },
        { 0 , "Cutlass" },
        { 1 , "Lantern" },
        { 2 , "Crossbow" },
        { 3 , "Blunderbuss" },
        { 4 , "Flintlock" },
        { 5 , "Flaming Cutlass" },
        { 6 , "Flaming Crossbow" },
        { 7 , "Ultra Cutlass" },
        { 8 , "Staff of the Dead" },
        { 9 , "Flamethrower" },
        { 10 , "Double Blunderbuss" },
        { 11 , "Candle" },
        { 12 , "Scythe" },
        { 13 , "Bayonet" },
        { 14 , "Tentacle Whip" },
        { 15 , "Assault Rifle" },
        { 16 , "Sniper Bow" },
        { 17 , "Flaming Flintlock" },
        { 18 , "Flaming Axe" },
        { 19 , "Blade Gun" },
        { 20 , "Cursed Skull" },
        { 21 , "Harpoon" },
        { 22 , "Tranquilizer" },
        { 23 , "Soul Collector" },
        { 24 , "Alien Gun" },
    };
    private Dictionary<string, int> NameToIndex = new Dictionary<string, int>();

    private List<string> BaseWeapons = new List<string>
    {
        "Cutlass",
        "Lantern",
        "Crossbow",
        "Blunderbuss",
        "Flintlock",
        "Staff of the Dead"
    };

    private Dictionary<(string, string), string> Combinations = new Dictionary<(string, string), string>
    {
        // Flaming Cutlass
        {("Cutlass", "Lantern"), "Flaming Cutlass" }, {("Lantern", "Cutlass"), "Flaming Cutlass" },

        // Flaming Crossbow
        { ("Crossbow", "Lantern"), "Flaming Crossbow" }, {("Lantern", "Crossbow"), "Flaming Crossbow" },

        // Ultra Cutlass
        {("Cutlass", "Cutlass"), "Ultra Cutlass" },

        // Flamethrower
        {("Blunderbuss", "Lantern"), "Flamethrower" }, {("Lantern", "Blunderbuss"), "Flamethrower" },

        // Double Blunderbuss
        {("Blunderbuss", "Blunderbuss"), "Double Blunderbuss"},

        // Candle
        {("Lantern", "Lantern"), "Candle" },

        // Scythe
        {("Cutlass", "Staff of the Dead"), "Scythe" }, {("Staff of the Dead", "Cutlass"), "Scythe" },

        // Bayonet
        {("Cutlass", "Blunderbuss"), "Bayonet" }, {("Blunderbuss", "Cutlass"), "Bayonet" },

        // Assault Rifle
        {("Flintlock", "Flintlock"), "Assault Rifle" },

        // Sniper Bow
        {("Crossbow", "Crossbow"), "Sniper Bow" },

        // Flaming Flintlock
        {("Flintlock", "Lantern"), "Flaming Flintlock" }, {("Lantern", "Flintlock"), "Flaming Flintlock" },

        // Flaming Axe
        {("Staff of the Dead", "Lantern"), "Flaming Axe" }, {("Lantern", "Staff of the Dead"), "Flaming Axe"},

        // Blade Gun
        {("Cutlass", "Flintlock"), "Blade Gun" }, {("Flintlock", "Cutlass"), "Blade Gun" },

        // Cursed Skull
        {("Staff of the Dead", "Staff of the Dead"), "Cursed Skull" },

        // Harpoon
        {("Crossbow", "Blunderbuss"), "Harpoon" }, {("Blunderbuss", "Crossbow"), "Harpoon"},

        // Tranquilizer
        //{("Flintlock", "Staff of the Dead"), "Tranquilizer" }, {("Staff of the Dead", "Flintlock"), "Tranquilizer"}
        {("Crossbow", "Staff of the Dead"), "Tranquilizer" }, {("Staff of the Dead", "Crossbow"), "Tranquilizer"},

        // Soul Collector
        {("Blunderbuss", "Staff of the Dead"), "Soul Collector" }, {("Staff of the Dead", "Blunderbuss"), "Soul Collector"},

        // Alien Gun
        {("Blunderbuss", "Flintlock"), "Alien Gun" }, {("Flintlock", "Blunderbuss"), "Alien Gun"},

        //{("Cursed Skull", "Cursed Skull"), "Cursed Skull" }
    };

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetRandomBaseWeaponID()
    {
        string weapon = BaseWeapons[Random.Range(0, BaseWeapons.Count)];
        return NameToIndex[weapon];
    }

    public int GetRandomWeaponID()
    {
        return Random.Range(0, WeaponIndex.Count - 1);
    }

    public GameObject InstantiateBaseWeapon()
    {
        string weapon = BaseWeapons[Random.Range(0, BaseWeapons.Count)];
        GameObject wp = Instantiate(Resources.Load(RESOURCES_PATH + weapon, typeof(GameObject))) as GameObject;
        return wp;
    }

    public GameObject InstantiateWeapon(int id)
    {
        if (id == -1) return null;
        GameObject wp = Instantiate(Resources.Load(RESOURCES_PATH + WeaponIndex[id], typeof(GameObject))) as GameObject;
        return wp;
    }

    public GameObject InstantiateCombination(int currID, int nextID)
    {
        string curr = WeaponIndex[currID];
        string next = WeaponIndex[nextID];
        GameObject wp;
        if (curr == null || curr.Equals("Empty") || !Combinable(currID, nextID))
        {
            wp = InstantiateWeapon(nextID);
        }
        else
        {
            wp = Instantiate(Resources.Load(RESOURCES_PATH + Combinations[(curr, next)], typeof(GameObject))) as GameObject;
        }
        return wp;
    }

    public bool IsBaseWeapon(int id)
    {
        return BaseWeapons.Contains(GetName(id));
    }

    public bool IsBaseWeapon(string id)
    {
        return BaseWeapons.Contains(id);
    }

    public bool Combinable(int id1, int id2)
    {
        return Combinations.ContainsKey((GetName(id1), GetName(id2)));
    }

    public bool Combinable(string id1, string id2)
    {
        return Combinations.ContainsKey((id1, id2));
    }

    public string GetName(int i)
    {
        if (!WeaponIndex.ContainsKey(i))
        {
            return "Empty";
        }
        return WeaponIndex[i];
    }

    public int GetIndex(string s)
    {
        if (!NameToIndex.ContainsKey(s))
        {
            return -1;
        }
        return NameToIndex[s];
    }

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        foreach (int i in WeaponIndex.Keys)
        {
            NameToIndex.Add(WeaponIndex[i], i);
        }
    }
}