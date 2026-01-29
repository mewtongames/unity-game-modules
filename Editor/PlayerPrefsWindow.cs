using System;
using System.Collections.Generic;
using System.Linq;
using MewtonGames.DataStorage.Providers;
using UnityEditor;
using UnityEngine;

namespace MewtonGames.Editor
{
    public class PlayerPrefsWindow : EditorWindow
    {
        private Vector2 _scrollPosition;
        private List<PlayerPrefsEntry> _entries;
        private string _newKey = string.Empty;
        private PlayerPrefsType _newType = PlayerPrefsType.String;
        private string _searchFilter = string.Empty;


        [MenuItem("Mewton Games/PlayerPrefs Editor")]
        public static void ShowWindow()
        {
            var window = GetWindow<PlayerPrefsWindow>("PlayerPrefs Editor");
            window.minSize = new Vector2(400f, 300f);
        }


        private void OnEnable()
        {
            _entries = new List<PlayerPrefsEntry>();
            LoadRegisteredKeys();
            RefreshAllValues();
        }

        private void OnGUI()
        {
            DrawToolbar();
            DrawSearchBar();
            DrawEntriesList();
            DrawAddEntryPanel();
        }


        private void DrawToolbar()
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);

            if (GUILayout.Button("Refresh", EditorStyles.toolbarButton, GUILayout.Width(60)))
            {
                LoadRegisteredKeys();
                RefreshAllValues();
            }

            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Delete All", EditorStyles.toolbarButton, GUILayout.Width(70)))
            {
                if (EditorUtility.DisplayDialog("Delete All PlayerPrefs",
                    "Are you sure you want to delete ALL PlayerPrefs data? This cannot be undone.",
                    "Delete All", "Cancel"))
                {
                    PlayerPrefsWrapper.DeleteAll();
                    PlayerPrefsWrapper.Save();
                    RefreshAllValues();
                }
            }

            EditorGUILayout.EndHorizontal();
        }

        private void DrawSearchBar()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Search:", GUILayout.Width(50));
            _searchFilter = EditorGUILayout.TextField(_searchFilter);
            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                _searchFilter = string.Empty;
                GUI.FocusControl(null);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(5);
        }

        private void DrawEntriesList()
        {
            var scrollViewStyle = new GUIStyle { padding = new RectOffset(5, 5, 5, 5) };
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, scrollViewStyle, GUILayout.ExpandWidth(true));

            var filteredEntries = string.IsNullOrEmpty(_searchFilter)
                ? _entries
                : _entries.Where(e => e.key.IndexOf(_searchFilter, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

            if (filteredEntries.Count == 0)
            {
                EditorGUILayout.HelpBox("No PlayerPrefs entries found. Add a new entry below.", MessageType.Info);
            }
            else
            {
                DrawHeader();

                foreach (var entry in filteredEntries.ToList())
                {
                    DrawEntry(entry);
                }
            }

            EditorGUILayout.EndScrollView();
        }

        private void DrawHeader()
        {
            var headerStyle = new GUIStyle(EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Key", headerStyle, GUILayout.MinWidth(100));
            EditorGUILayout.LabelField("Type", headerStyle, GUILayout.Width(60));
            EditorGUILayout.LabelField("Value", headerStyle, GUILayout.MinWidth(100));
            GUILayout.Space(70);
            EditorGUILayout.EndHorizontal();

            var rect = EditorGUILayout.GetControlRect(false, 1);
            EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 0.5f));
            EditorGUILayout.Space(2);
        }

        private void DrawEntry(PlayerPrefsEntry entry)
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(entry.key, GUILayout.MinWidth(100));

            var newType = (PlayerPrefsType)EditorGUILayout.EnumPopup(entry.type, GUILayout.Width(60));
            if (newType != entry.type)
            {
                entry.type = newType;
                entry.RefreshValue();
            }

            DrawValueField(entry);

            if (GUILayout.Button("Save", GUILayout.Width(45)))
            {
                entry.SaveValue();
                PlayerPrefsWrapper.Save();
            }

            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                if (EditorUtility.DisplayDialog("Delete PlayerPref",
                    $"Delete key '{entry.key}'?", "Delete", "Cancel"))
                {
                    PlayerPrefsWrapper.DeleteKey(entry.key);
                    PlayerPrefsWrapper.Save();
                    PlayerPrefsWrapper.RemoveKey(entry.key);
                    _entries.Remove(entry);
                }
            }

            EditorGUILayout.EndHorizontal();
        }

        private void DrawValueField(PlayerPrefsEntry entry)
        {
            switch (entry.type)
            {
                case PlayerPrefsType.String:
                    entry.stringValue = EditorGUILayout.TextField(entry.stringValue, GUILayout.MinWidth(100));
                    break;
                case PlayerPrefsType.Int:
                    entry.intValue = EditorGUILayout.IntField(entry.intValue, GUILayout.MinWidth(100));
                    break;
                case PlayerPrefsType.Float:
                    entry.floatValue = EditorGUILayout.FloatField(entry.floatValue, GUILayout.MinWidth(100));
                    break;
            }
        }

        private void DrawAddEntryPanel()
        {
            EditorGUILayout.Space(10);

            var rect = EditorGUILayout.GetControlRect(false, 1);
            EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 0.5f));

            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("Add New Entry", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Key:", GUILayout.Width(35));
            _newKey = EditorGUILayout.TextField(_newKey, GUILayout.MinWidth(100));
            EditorGUILayout.LabelField("Type:", GUILayout.Width(35));
            _newType = (PlayerPrefsType)EditorGUILayout.EnumPopup(_newType, GUILayout.Width(60));

            GUI.enabled = !string.IsNullOrEmpty(_newKey) && !_entries.Any(e => e.key == _newKey);
            if (GUILayout.Button("Add", GUILayout.Width(50)))
            {
                PlayerPrefsWrapper.AddKey(_newKey);
                var entry = new PlayerPrefsEntry(_newKey, _newType);
                _entries.Add(entry);
                _newKey = string.Empty;
            }
            GUI.enabled = true;

            EditorGUILayout.EndHorizontal();

            if (_entries.Any(e => e.key == _newKey) && !string.IsNullOrEmpty(_newKey))
            {
                EditorGUILayout.HelpBox("Key already exists in the list.", MessageType.Warning);
            }
        }


        private void LoadRegisteredKeys()
        {
            _entries.Clear();
            var keys = PlayerPrefsWrapper.GetRegisteredKeys();

            foreach (var key in keys)
            {
                _entries.Add(new PlayerPrefsEntry(key, PlayerPrefsType.String));
            }
        }

        private void RefreshAllValues()
        {
            foreach (var entry in _entries)
            {
                entry.RefreshValue();
            }
        }


        private enum PlayerPrefsType
        {
            String,
            Int,
            Float
        }

        private class PlayerPrefsEntry
        {
            public string key { get; }
            public PlayerPrefsType type { get; set; }
            public string stringValue { get; set; }
            public int intValue { get; set; }
            public float floatValue { get; set; }

            public PlayerPrefsEntry(string key, PlayerPrefsType type)
            {
                this.key = key;
                this.type = type;
                RefreshValue();
            }

            public void RefreshValue()
            {
                switch (type)
                {
                    case PlayerPrefsType.String:
                        stringValue = PlayerPrefsWrapper.GetString(key, string.Empty);
                        break;
                    case PlayerPrefsType.Int:
                        intValue = PlayerPrefsWrapper.GetInt(key, 0);
                        break;
                    case PlayerPrefsType.Float:
                        floatValue = PlayerPrefsWrapper.GetFloat(key, 0f);
                        break;
                }
            }

            public void SaveValue()
            {
                switch (type)
                {
                    case PlayerPrefsType.String:
                        PlayerPrefsWrapper.SetString(key, stringValue);
                        break;
                    case PlayerPrefsType.Int:
                        PlayerPrefsWrapper.SetInt(key, intValue);
                        break;
                    case PlayerPrefsType.Float:
                        PlayerPrefsWrapper.SetFloat(key, floatValue);
                        break;
                }
            }
        }
    }
}
