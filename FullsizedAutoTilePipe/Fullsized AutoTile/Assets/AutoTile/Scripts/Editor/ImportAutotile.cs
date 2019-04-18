using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.IO;


public static class ImportAutotile
{

    static public void ImportFullsizeAsset(Object _image, AutoTileLookup _lookup)
    {
        Texture2D selectedTexture = _image as Texture2D;
        ProcessFullsizeTexture(selectedTexture, _lookup);
    }

    public static RuleTile.TilingRule GetRule(int neighbourMask)
    {

        RuleTile.TilingRule m_rule = new RuleTile.TilingRule();
        m_rule.m_Neighbors = new int[8];

        int mask = 128;
        for (int i = 7; i >= 0; i--)
        {
            if (neighbourMask >= mask)
            {
                m_rule.m_Neighbors[i] = 1;
                neighbourMask -= mask;
            }
            else
            {
                m_rule.m_Neighbors[i] = 0;
            }
            mask /= 2;
        }

        //discard corner tiles if their cardinal neighbours are not needed.
        if (m_rule.m_Neighbors[0] == 1 &&
        (m_rule.m_Neighbors[1] == 0 ||
            m_rule.m_Neighbors[3] == 0))
        {
            return null;
        }

        if (m_rule.m_Neighbors[2] == 1 &&
        (m_rule.m_Neighbors[1] == 0 ||
            m_rule.m_Neighbors[4] == 0))
        {
            return null;
        }

        if (m_rule.m_Neighbors[5] == 1 &&
        (m_rule.m_Neighbors[3] == 0 ||
            m_rule.m_Neighbors[6] == 0))
        {
            return null;
        }

        if (m_rule.m_Neighbors[7] == 1 &&
        (m_rule.m_Neighbors[4] == 0 ||
            m_rule.m_Neighbors[6] == 0))
        {
            return null;
        }
        return m_rule;

    }

    #region FULLSIZE
    static void ProcessFullsizeTexture(Texture2D selectedTexture, AutoTileLookup lookup)
    {
        string assetName = selectedTexture.name;

        if (!AssetDatabase.IsValidFolder("Assets/Autotiles"))
        {
            string rootGuid = AssetDatabase.CreateFolder("Assets", "Autotiles");
        }

        string folderGuid = AssetDatabase.CreateFolder("Assets/Autotiles", assetName);

        string folderName = AssetDatabase.GUIDToAssetPath(folderGuid);

        Vector2Int TileResolution = new Vector2Int(
            selectedTexture.width / 4,
            selectedTexture.height / 6);



        AssetDatabase.Refresh();



        RuleTile m_tile = ScriptableObject.CreateInstance<RuleTile>();

        m_tile.m_TilingRules = new List<RuleTile.TilingRule>();


        int count = 0;
        for (int i = 255; i >= 0; i--)
        {
            RuleTile.TilingRule rule = GetRule(i);
            if (rule != null)
            {
                m_tile.m_TilingRules.Add(rule);
                GenerateSprites(count, TileResolution.x, selectedTexture, lookup, folderName);
                count++;
            }
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        for (int i = 0; i < m_tile.m_TilingRules.Count; i++)
        {
            m_tile.m_TilingRules[i].m_Sprites = new Sprite[1];
            m_tile.m_TilingRules[i].m_Sprites[0] = AssetDatabase.LoadAssetAtPath<Sprite>(folderName + "/" + selectedTexture.name + i + ".png");
        }

        AssetDatabase.CreateAsset(m_tile, folderName + "/" + assetName + "tile.asset");

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    static void GenerateSprites(int neighbourMask, int res, Texture2D source, AutoTileLookup lookup, string folderPath)
    {




        //pull the asset coords from our lookup table.
        int TL = lookup.m_TileQuads[neighbourMask * 4];
        int TR = lookup.m_TileQuads[(neighbourMask * 4) + 1];
        int BL = lookup.m_TileQuads[(neighbourMask * 4) + 2];
        int BR = lookup.m_TileQuads[(neighbourMask * 4) + 3];

        //from Tile IDs to SubtileIDs
        TL = 20 - (TL * 4) + ((TL % 2) * 6);
        TR = 20 - (TR * 4) + ((TR % 2) * 6);
        BL = 20 - (BL * 4) + ((BL % 2) * 6);
        BR = 20 - (BR * 4) + ((BR % 2) * 6);
        TR += 1;
        BL -= 4;
        BR -= 3;


        Vector2Int TLcoord = new Vector2Int(TL % 4, TL / 4);
        Vector2Int TRcoord = new Vector2Int(TR % 4, TR / 4);
        Vector2Int BLcoord = new Vector2Int(BL % 4, BL / 4);
        Vector2Int BRcoord = new Vector2Int(BR % 4, BR / 4);




        Texture2D tex = new Texture2D(res * 2, res * 2);
        Vector2Int localIndex = new Vector2Int(0, 0);
        for (int texX = 0; texX < tex.width / 2; texX++)
        {
            localIndex.y = 0;
            for (int texY = res; texY < res * 2; texY++)
            {

                Color sample = source.GetPixel(
                     (TLcoord.x * res) + localIndex.x,
                     (TLcoord.y * res) + localIndex.y);
                tex.SetPixel(texX, texY, sample);
                localIndex.y++;

            }

            localIndex.y = 0;
            for (int texY = 0; texY < res; texY++)
            {

                Color sample = source.GetPixel(
                     (BLcoord.x * res) + localIndex.x,
                     (BLcoord.y * res) + localIndex.y);
                tex.SetPixel(texX, texY, sample);
                localIndex.y++;

            }
            localIndex.x++;

        }

        localIndex.x = 0;
        for (int texX = res; texX < res * 2; texX++)
        {
            localIndex.y = 0;
            for (int texY = res; texY < res * 2; texY++)
            {

                Color sample = source.GetPixel(
                     (TRcoord.x * res) + localIndex.x,
                     (TRcoord.y * res) + localIndex.y);
                tex.SetPixel(texX, texY, sample);
                localIndex.y++;

            }

            localIndex.y = 0;
            for (int texY = 0; texY < res; texY++)
            {

                Color sample = source.GetPixel(
                     (BRcoord.x * res) + localIndex.x,
                     (BRcoord.y * res) + localIndex.y);
                tex.SetPixel(texX, texY, sample);
                localIndex.y++;

            }
            localIndex.x++;

        }


        byte[] bytes = tex.EncodeToPNG();
        File.WriteAllBytes(folderPath + "/" + source.name + neighbourMask + ".png", bytes);
    }

    #endregion
}
