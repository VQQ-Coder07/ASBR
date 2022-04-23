using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEditor;
public class Editor : MonoBehaviour
{
    public int levelId;
    public Map[] maps;
    public GameObject prefab;
    public Terrain goTerrain;
    public Transform parent, demoCam, Cam, tileButtons;
    public enum mode
    {
        move, build, delete, spawnpoint, play
    }
    public mode _mode;
    private mode lastMode;
    public GameObject[] buttons, messages, tiles;
    public static Editor instance;
    public TMP_InputField name;
    public Color normal, selected;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Load();
        SelectTile(0);
        LoadTileThumbnails();
    }
    public void Switch()
    {
        _mode++;
        foreach (GameObject obj in buttons)
        {
            obj.SetActive(false);
        }
        switch (_mode)
        {
            case mode.move:
            buttons[0].SetActive(true);
            return;
            case mode.build:
            buttons[1].SetActive(true);
            return;
            case mode.delete:
            buttons[2].SetActive(true);
            return;
            case mode.spawnpoint:
            buttons[3].SetActive(true);
            return;
            case mode.play:
            buttons[4].SetActive(true);
            return;
        }
    }
    public void ResetMode()
    {
        _mode = mode.move;
        foreach (GameObject obj in buttons)
        {
            obj.SetActive(false);
        }
        buttons[0].SetActive(true);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast (ray,out hit, 200) && Editor.instance._mode == Editor.mode.delete)
            {
                if(hit.transform.CompareTag("Tile"))
                {
                    Destroy(hit.transform.gameObject);
                }
            }
            if (goTerrain.GetComponent<Collider>().Raycast (ray, out hit, Mathf.Infinity) && Editor.instance._mode == Editor.mode.build) 
            {
                Instantiate(prefab, hit.point, Quaternion.identity, parent);
            }
            if(Physics.Raycast (ray,out hit, 200) && Editor.instance._mode == Editor.mode.spawnpoint)
            {
                if(hit.transform.CompareTag("Tile"))
                {
                    Tile tile = hit.transform.gameObject.GetComponent<Tile>();
                    if(tile.spawnpoint != null)
                    {
                        tile.spawnpoint.SetActive(!tile.spawnpoint.activeSelf);
                    }
                }
            }
        }
    }
    public void Save()
    {
        //if(Validate())
        {
            maps[levelId].name = name.text;
            for(int j=0; j < parent.childCount; j++)
            {
                Tile t = parent.GetChild(j).GetComponent<Tile>();
                maps[levelId].ids[j] = t.id;
                maps[levelId].poses[j] = t.pos;
                maps[levelId].rots[j] = t.rot;
                if(t.spawnpoint != null)
                {
                    if(t.spawnpoint.activeSelf)
                    maps[levelId].spawnpoints[j] = true;
                    else
                    maps[levelId].spawnpoints[j] = false;
                }

            }
        }

    }
    public void Load()
    {
        name.text = maps[levelId].name;
        for(int j=0; j < maps[levelId].ids.Length; j++)
        {
            if(maps[levelId].ids[j] != 0)
            {
                GameObject obj = Instantiate(tiles[maps[levelId].ids[j]], maps[levelId].poses[j], maps[levelId].rots[j], parent);
                if(maps[levelId].spawnpoints[j] == true)
                {
                    obj.GetComponent<Tile>().spawnpoint.SetActive(true);
                }
            }
        }
    }
    private bool Validate()
    {
        bool value = true;
        int points = 0;
        messages[0].transform.parent.gameObject.SetActive(true);
        foreach(GameObject obj in messages)
        {
            obj.SetActive(false);
        }
        for(int j=0; j < parent.childCount; j++)
        {
            if(parent.GetChild(j).GetComponent<Tile>().spawnpoint != null)
            points++;
        }
        if(points != 10)
        {
            messages[0].SetActive(true);
            value = false;
        }
        if(ConsistsOfWhiteSpace(maps[levelId].name))
        {
            messages[1].SetActive(true);
            value = false;
        }
        if(value == true)
        {
            messages[2].SetActive(true);
        }
        return value;
    }
    private bool ConsistsOfWhiteSpace(string s)
    {
        foreach(char c in s)
        {
            if(c != ' ') return false;
        }
        return true;
    }
    public void SelectTile(int id)
    {
        prefab = tiles[id + 1];
        foreach(Transform child in tileButtons)
        {
            child.GetComponent<Image>().color = normal;
        }
        tileButtons.GetChild(id).GetComponent<Image>().color = selected;
    }
    private void LoadTileThumbnails()
    {
        for(int j=0;j<tileButtons.childCount;j++)
        {
            tileButtons.GetChild(j).GetChild(0).GetChild(0).GetComponent<RawImage>().texture = AssetPreview.GetAssetPreview(tiles[j+1]);
        }
    }
    public void Play(bool value)
    {
        if(value)
        {
            lastMode = _mode;
            _mode = mode.play;
        }
        else
        {
            _mode = lastMode;
        }
    }
}