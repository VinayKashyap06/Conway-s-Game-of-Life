﻿using System.Collections;
using System.Collections.Generic;
using Commons;
using GameSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UiSystem
{
    public class ControllerUI : MonoBehaviour
    {
        [SerializeField]
        private Button startButton;
        [SerializeField]
        private Button stopButton;
        [SerializeField]
        private TMP_InputField xInputField;
        [SerializeField]
        private TMP_InputField yInputField;

        private void Start()
        {

            //Add listeners
            startButton.onClick.AddListener(()=> 
            {
                ServiceGame.Instance.StartSimulation();
                startButton.interactable = false;
            });

            stopButton.onClick.AddListener(()=> 
            {
               ServiceGame.Instance.StopSimulation();
               startButton.interactable = true;
            });

            xInputField.onValueChanged.AddListener((string value)=>
            {
                Globals.GridSizeX = int.Parse(value);
                Debug.Log("Value"+Globals.GridSizeX);
            });
            yInputField.onValueChanged.AddListener((string value)=> 
            {
                Globals.GridSizeY = int.Parse(value);
                Debug.Log("Value" + Globals.GridSizeY);
            });
        }       
    }
}