using UnityEngine;

public class LevelsContoller : MonoBehaviour
{
    public LevelSelectionPoint[] levels;

    public LevelSelectionPoint point1;
    public LevelSelectionPoint point2;

    public GameObject checkmark;

    public void Awake()
    {
        FindObjectOfType<SelectionController>().firstPoint =
            Game.PlayerStats.levelsUnlocked < 11 ?
                levels[Game.PlayerStats.levelsUnlocked - 1] : 
                levels[9];

        for (int i = Game.PlayerStats.levelsUnlocked; i < levels.Length; i++) {
            levels[i].level = -1;
            levels[i].gameObject.SetActive(false);
        }

        DisableFurtherSelection();
        AttachCheckmarks();
    }
	
	private void DisableFurtherSelection()
    {
        switch (Game.PlayerStats.levelsUnlocked)
        {
            case 1:
                levels[0].up = null;
                break;
            case 2:
                levels[1].right = null;
                break;
            case 3:
                levels[2].up = null;
                levels[2].right = null;
                levels[2].down = null;
                break;
            case 4:
                levels[2].down = null;
                levels[2].right = null;
                break;
            case 5:
                levels[2].right = null;
                break;
            case 6:
                point1.down = null;
                point2.right = null;
                break;
            case 7:
                point1.down = null;
                break;
            case 8:
                levels[7].right = null;
                break;
            case 9:
                levels[8].left = null;
                break;
        }
    }

    private void AttachCheckmarks()
    {
        for (int i = 0; i < Game.PlayerStats.levelsUnlocked - 1; i++)
        {
            var sprite = levels[i].GetComponent<SpriteRenderer>();

            var go = Instantiate(checkmark,
                new Vector3(
                    sprite.transform.position.x + sprite.bounds.extents.x - 0.17f,
                    sprite.transform.position.y - sprite.bounds.extents.y + 0.25f,
                    0.0f
                ),
                Quaternion.identity,
                sprite.transform
            ) as GameObject;

            go.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
