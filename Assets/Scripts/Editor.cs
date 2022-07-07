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
    public GameObject prefab, player, camdemo;
    public Terrain goTerrain;
    public Transform parent, demoCam, Cam, tileButtons;
    public enum mode
    {
        move, build, delete, spawnpoint, play
    }
    public mode _mode;
    private mode lastMode;
    public GameObject[] buttons, messages, tiles, buildTools, editor, playButtons, playerUI;
    public static Editor instance;
    public TMP_InputField name;
    public Color normal, selected, selectedMode, unselectedMode;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        levelId = PlayerPrefs.GetInt("selectedMap");
        Load();
        SelectTile(0);
        SetMode(0);
        LoadTileThumbnails();
        if(player)
        player.SetActive(false);
    }
    public void SetMode(int newmode)
    {
        foreach(GameObject obj in buildTools)
        obj.SetActive(false);
        switch (newmode)
        {
            case 0:
                _mode = mode.move;
                break;
            case 1:
                _mode = mode.build;
                foreach (GameObject obj in buildTools)
                obj.SetActive(true);
                break;
            case 2:
                _mode = mode.delete;
                break;
            case 3:
                _mode = mode.spawnpoint;
                break;
        }
        foreach(GameObject obj in buttons)
        {
            obj.GetComponent<Image>().color = unselectedMode;
        }
        buttons[newmode].GetComponent<Image>().color = selectedMode;
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
        //LoadTileThumbnails();

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
        if(levelId == 0)
        {
            return;
        }
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
            tileButtons.GetChild(j).GetChild(0).GetChild(0).GetComponent<RawImage>().texture = RuntimePreviewGenerator.GenerateModelPreview(tiles[j+1].transform, 128, 128, false, true );
        //AssetPreview.GetAssetPreview(tiles[j+1]);
    }
    }
    private GameObject[] spawnpoints;
    Transform GetClosestSpawnpoint(Transform[] _spawnpoints)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in _spawnpoints)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    public void Play(bool value)
    {
        if(value)
        {
            spawnpoints = GameObject.FindGameObjectsWithTag("spawnpoint");
            float dist = 10000;
            Transform spawnpoint =  null;
            foreach (GameObject obj in spawnpoints)
            {
                if (Vector3.Distance(camdemo.transform.position, obj.transform.position) < dist)
                {
                    spawnpoint = obj.transform;
                    dist = Vector3.Distance(camdemo.transform.position, obj.transform.position);
                }
                obj.SetActive(false);
            }
            if (!spawnpoint)
            {
                Debug.LogError("Error: no spawnpoints");
                return;
            }
            else
            {
                player.transform.position = spawnpoint.position;
                lastMode = _mode;
                _mode = mode.play;
                foreach (GameObject obj in editor)
                    obj.SetActive(false);
                playButtons[0].SetActive(false);
                playButtons[1].SetActive(true);
                foreach (GameObject obj in playerUI)
                    obj.SetActive(true);
                player.SetActive(true);
            }

        }
        else
        {
            foreach (GameObject obj in spawnpoints)
            {
                obj.SetActive(true);
            }
            _mode = lastMode;
            foreach (GameObject obj in editor)
                obj.SetActive(true);
            playButtons[0].SetActive(true);
            playButtons[1].SetActive(false);
            foreach(GameObject obj in playerUI)
                obj.SetActive(false);
            player.SetActive(false);
        }
    }
}