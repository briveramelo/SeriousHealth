﻿/**
* Copyright 2015 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

//#define ENABLE_DEBUGGING

using IBM.Watson.DeveloperCloud.DataTypes;
using IBM.Watson.DeveloperCloud.Services.SpeechToText.v1;
using UnityEngine;
using UnityEngine.UI;
using IBM.Watson.DeveloperCloud.Utilities;
using IBM.Watson.DeveloperCloud.Widgets;
using System.Collections.Generic;
using System.Linq;

public enum Evaluation {
    Bad = 0,
    OK = 1,
    Great = 2
}
#pragma warning disable 414

    /// <summary>
    /// SpeechToText Widget that wraps the SpeechToText service.
    /// </summary>
public class SpeechEvaluator : Widget {

    //public reference for other classes to access
    public static SpeechEvaluator Instance;

    #region Inputs
    [SerializeField]
    private Input m_AudioInput = new Input("Audio", typeof(AudioData), "OnAudio");
    [SerializeField]
    private Input m_LanguageInput = new Input("Language", typeof(LanguageData), "OnLanguage");
    #endregion

    #region Outputs
    [SerializeField]
    private Output m_ResultOutput = new Output(typeof(SpeechToTextData), true);
    #endregion

    #region Private Data
    private SpeechToText m_SpeechToText = new SpeechToText();
    [SerializeField]
    private Text m_StatusText = null;
    [SerializeField]
    private bool m_DetectSilence = true;
    [SerializeField]
    private float m_SilenceThreshold = 0.03f;
    [SerializeField]
    private bool m_WordConfidence = false;
    [SerializeField]
    private bool m_TimeStamps = false;
    [SerializeField]
    private int m_MaxAlternatives = 1;
    [SerializeField]
    private bool m_EnableContinous = false;
    [SerializeField]
    private bool m_EnableInterimResults = false;
    [SerializeField]
    private Text m_Transcript = null;
    [SerializeField, Tooltip("Language ID to use in the speech recognition model.")]
    private string m_Language = "en-US";
    #endregion

    #region Public Properties
    /// <summary>
    /// This property starts or stop's this widget listening for speech.
    /// </summary>
    public bool Active {
        get { return m_SpeechToText.IsListening; }
        set {
            if (value && !m_SpeechToText.IsListening) {
                m_SpeechToText.DetectSilence = m_DetectSilence;
                m_SpeechToText.EnableWordConfidence = m_WordConfidence;
                m_SpeechToText.EnableTimestamps = m_TimeStamps;
                m_SpeechToText.SilenceThreshold = m_SilenceThreshold;
                m_SpeechToText.MaxAlternatives = m_MaxAlternatives;
                m_SpeechToText.EnableContinousRecognition = m_EnableContinous;
                m_SpeechToText.EnableInterimResults = m_EnableInterimResults;
                m_SpeechToText.OnError = OnError;
                m_SpeechToText.StartListening(OnRecognize);
                if (m_StatusText != null)
                    m_StatusText.text = "LISTENING";
            }
            else if (!value && m_SpeechToText.IsListening) {
                m_SpeechToText.StopListening();
                if (m_StatusText != null)
                    m_StatusText.text = "READY";
            }
        }
    }
    #endregion

    #region Widget Interface
    /// <exclude />
    protected override string GetName() {
        return "SpeechToText";
    }
    #endregion

    #region Intialization
    protected override void Start() {
        base.Start();

        if (m_StatusText != null)
            m_StatusText.text = "READY";
        if (!m_SpeechToText.GetModels(OnGetModels))
            Debug.LogError("Failed to request models.");
    }

    protected override void Awake() {
        base.Awake();
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
    private void OnGetModels(Model[] models) {
        //Debug.Log("getting models!");
        if (models != null) {
            Model bestModel = null;
            foreach (var model in models) {
                if (model.language.StartsWith(m_Language)
                    && (bestModel == null || model.rate > bestModel.rate)) {
                    bestModel = model;
                }
            }

            if (bestModel != null) {
                //Debug.Log("Selecting Recognize Model: " + bestModel.name);
                m_SpeechToText.RecognizeModel = bestModel.name;
            }
        }
    }
    #endregion


    #region Event handlers
    /// <summary>
    /// Button handler to toggle the active state of this widget.
    /// </summary>
    public void OnListenButton() {
        Active = !Active;
    }

    /// <exclude />

    private void OnDisable() {
        if (Active)
            Active = false;
    }

    private void OnError(string error) {
        Active = false;
        if (m_StatusText != null)
            m_StatusText.text = "ERROR: " + error;
    }

    private void OnAudio(Data data) {
        if (!Active)
            Active = true;

        m_SpeechToText.OnListen((AudioData)data);
    }

    private void OnLanguage(Data data) {
        LanguageData language = data as LanguageData;
        if (language == null)
            throw new WatsonException("Unexpected data type");

        if (!string.IsNullOrEmpty(language.Language)) {
            m_Language = language.Language;

            if (!m_SpeechToText.GetModels(OnGetModels))
                Debug.LogError("Failed to rquest models.");
        }
    }


    private void OnRecognize(SpeechRecognitionEvent result) {
        
        m_ResultOutput.SendData(new SpeechToTextData(result));

        if (result != null && result.results.Length > 0) {
            if (m_Transcript != null)
                m_Transcript.text = "";

            foreach (var res in result.results) {
                if (res.keywords_result != null) {
                    if (res.keywords_result.keyword != null) {
                        foreach (var keyword in res.keywords_result.keyword) {
                            //UnityEngine.Debug.Log("playing");
                            //AudioData myData = new AudioData();
                            //AudioClip myClip = m_SpeechToText.GetSpeechRecordings().First().Clip;
                            //float[] myData = new float[myClip.samples * myClip.channels];
                            //myClip.GetData(myData, 0);
                            //myClip.SetData(, );
                            //AudioSource.PlayClipAtPoint(myClip, UnityEngine.Camera.main.transform.position);

                            m_Transcript.text += string.Format("{0} ({1}, {2:0.00})\n",
                                keyword.normalized_text, res.final ? "Final" : "Interim", keyword.confidence);
                        }
                    }
                }                
            }
        }
    }
    #endregion

    #region Custom interfaces
    public void UpdateKeywords(List<string> newKeywords) {
        m_SpeechToText.UpdateKeywords(newKeywords);
        //TO DO
        //check that it's not mid processing before updating the keywords
    }
    #endregion
}