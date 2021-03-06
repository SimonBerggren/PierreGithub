﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JournalScript : MonoBehaviour
{
    public Texture2D Background;
    public Font font;

    private int selectedQuest = 0;
    protected bool displaying;

    public bool activated;

    protected List<Quest> Quests;
    protected GUIStyle inCompletedStyle;
    protected GUIStyle completedStyle;

    void Awake()
    {
        Quests = new List<Quest>();

        inCompletedStyle = new GUIStyle();
        inCompletedStyle.normal.textColor = Color.black;
        inCompletedStyle.font = font;

        completedStyle = new GUIStyle();
        completedStyle.normal.textColor = Color.grey;
        completedStyle.font = font;
    }

    void Update()
    {
        if (Input.GetButtonDown("Journal") && activated)
            displaying = !displaying;
    }

    public void AddQuest(Quest Quest)
    {
        Quests.Add(Quest);
        Quest.IsCreated();
    }

    void OnGUI()
    {
        if (!activated)
            return;

        HandleMiniMe(ref displaying);

        if (!displaying)
            return;

        float texPositionX = Screen.width / 10;
        float texPositionY = Screen.height / 10;
        float texWidth = Screen.width * 0.8f;
        float texHeight = Screen.height * 0.8f;

        float offsetX = texWidth / 50;
        float offsetY = texHeight / 60;

        float abstractPosX = texPositionX + offsetX;
        float abstractPosY = texPositionY + offsetY;
        float abstractWidth = texWidth * 0.6f - offsetX;
        float abstractHeight = texHeight / 10;

        float descriptionPosX = abstractPosX + abstractWidth + (offsetX * 1.5f);
        float descriptionPosY = abstractPosY;
        float descriptionWidth = texWidth * 0.4f - offsetX * 3;
        float descriptionHeight = abstractHeight;

        GUI.DrawTexture(new Rect(texPositionX, texPositionY, texWidth, texHeight), Background);

        if (GUI.Button(new Rect(texPositionX  + texWidth - 40, texPositionY, 40, 40), "X"))
            displaying = false;

        for (int nr = 0; nr < Quests.Count; nr++)
        {
            GUI.skin.font = font;

            if (GUI.Button(new Rect(abstractPosX, abstractPosY + abstractPosY * nr, abstractWidth, abstractHeight), Quests[nr].Name))
                selectedQuest = nr;
        }


        for (int nr = 0; nr < Quests[selectedQuest].NrOfSteps; nr++)
        {
            GUILayout.BeginArea(new Rect(descriptionPosX, descriptionPosY + descriptionHeight * nr, descriptionWidth, descriptionHeight));

            if (Quests[selectedQuest].GetStep(nr).Completed)
                GUILayout.Label(Quests[selectedQuest].GetStep(nr).Objective, completedStyle);
            else
            {
                GUILayout.Label(Quests[selectedQuest].GetStep(nr).Objective + "\n" + Quests[selectedQuest].GetStep(nr).Description, inCompletedStyle);
                GUILayout.EndArea();
                break;
            }
            GUILayout.EndArea();
        }
    }
    void HandleMiniMe(ref bool displaying)
    {
        if (displaying)
            return;

        float Width = Screen.width / 20;
        float Height = Screen.height / 10;
        float X = Screen.width - Width;
        float Y = Height;

        if (GUI.Button(new Rect(X, Y, Width, Height), "Uppdrag"))
            displaying = true;
    }
}
