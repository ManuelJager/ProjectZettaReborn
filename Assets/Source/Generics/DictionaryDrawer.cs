using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityObject = UnityEngine.Object;

public abstract class DictionaryDrawer<TK, TV> : PropertyDrawer
{
    private SerializableDictionary<TK, TV> _Dictionary;
    private bool _Foldout;
    private const float kButtonWidth = 18f;
    private const float PropertyFieldHeight = 17f;

    // Get total height of drawer 
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        CheckInitialize(property, label);
        if (_Foldout)
        {
            return (_Dictionary.Count + 1) * PropertyFieldHeight;
        }
        return PropertyFieldHeight;
    }

    /// <summary>
    /// Draw GUI
    /// </summary>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        CheckInitialize(property, label);

        // set height of property field
        position.height = PropertyFieldHeight;

        // foldout button rect
        var foldoutRect = position;
        foldoutRect.width -= 2 * kButtonWidth;

        // get foldout value
        EditorGUI.BeginChangeCheck();
        _Foldout = EditorGUI.Foldout(foldoutRect, _Foldout, label, true);
        if (EditorGUI.EndChangeCheck())
        {
            EditorPrefs.SetBool(label.text, _Foldout);
        }

        // base button rect for dictionary control buttons (add / clear)
        var buttonRect = position;
        buttonRect.x = position.width - kButtonWidth + position.x;
        buttonRect.width = kButtonWidth + 2;

        // add item to dictionary button
        if (GUI.Button(buttonRect, new GUIContent("+", "Add item"), EditorStyles.miniButtonRight))
        {
            AddNewItem();
        }

        // shift rect position to left
        buttonRect.x -= kButtonWidth;

        // clear dictionary button
        if (GUI.Button(buttonRect, new GUIContent("X", "Clear dictionary"), EditorStyles.miniButtonLeft))
        {
            ClearDictionary();
        }

        // if not folded out, don't need to draw fields
        if (!_Foldout)
        {
            return;
        }

        // draw fields for each keyvaluepair
        foreach (var item in _Dictionary)
        {
            
            var key = item.Key;
            var value = item.Value;

            position.y += PropertyFieldHeight;

            // rect for key field
            var keyRect = position;
            keyRect.width /= 2;
            keyRect.width -= 4;

            // create field for key
            EditorGUI.BeginChangeCheck();
            var newKey = DoField(keyRect, typeof(TK), key);

            if (EditorGUI.EndChangeCheck())
            {
                try
                {
                    _Dictionary.Remove(key);
                    _Dictionary.Add(newKey, value);
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
                break;
            }

            // rect for value field
            var valueRect = position;
            valueRect.x = position.width / 2 + 15;
            valueRect.width = keyRect.width - kButtonWidth;

            // set value of key to value of property drawer
            EditorGUI.BeginChangeCheck();
            value = DoField(valueRect, typeof(TV), value);
            if (EditorGUI.EndChangeCheck())
            {
                _Dictionary[key] = value;
                break;
            }

            // create remote item button
            var removeRect = valueRect;
            removeRect.x = valueRect.xMax + 6;
            removeRect.width = kButtonWidth;
            if (GUI.Button(removeRect, new GUIContent("x", "Remove item"), EditorStyles.miniButton))
            {
                RemoveItem(key);
                break;
            }
        }
    }

    /// <summary>
    /// Remove item by key
    /// </summary>
    private void RemoveItem(TK key)
    {
        _Dictionary.Remove(key);
    }

    /// <summary>
    /// Make sure Dictionary is initialIzed
    /// </summary>
    private void CheckInitialize(SerializedProperty property, GUIContent label)
    {
        if (_Dictionary == null)
        {
            var target = property.serializedObject.targetObject;
            _Dictionary = fieldInfo.GetValue(target) as SerializableDictionary<TK, TV>;
            if (_Dictionary == null)
            {
                _Dictionary = new SerializableDictionary<TK, TV>();
                fieldInfo.SetValue(target, _Dictionary);
            }

            _Foldout = EditorPrefs.GetBool(label.text);
        }
    }

    // get editorGUI field for type
    private static readonly Dictionary<Type, Func<Rect, object, object>> _Fields =
        new Dictionary<Type, Func<Rect, object, object>>()
        {
            { typeof(int), (rect, value) => EditorGUI.IntField(rect, (int)value) },
            { typeof(float), (rect, value) => EditorGUI.FloatField(rect, (float)value) },
            { typeof(string), (rect, value) => EditorGUI.TextField(rect, (string)value) },
            { typeof(bool), (rect, value) => EditorGUI.Toggle(rect, (bool)value) },
            { typeof(Vector2), (rect, value) => EditorGUI.Vector2Field(rect, GUIContent.none, (Vector2)value) },
            { typeof(Vector3), (rect, value) => EditorGUI.Vector3Field(rect, GUIContent.none, (Vector3)value) },
            { typeof(Bounds), (rect, value) => EditorGUI.BoundsField(rect, (Bounds)value) },
            { typeof(Rect), (rect, value) => EditorGUI.RectField(rect, (Rect)value) },
        };

    // get property field for type
    private static T DoField<T>(Rect rect, Type type, T value)
    {
        Func<Rect, object, object> field;
        if (_Fields.TryGetValue(type, out field))
            return (T)field(rect, value);

        if (type.IsEnum)
            return (T)(object)EditorGUI.EnumPopup(rect, (Enum)(object)value);

        if (typeof(UnityObject).IsAssignableFrom(type))
            return (T)(object)EditorGUI.ObjectField(rect, (UnityObject)(object)value, type, true);

        Debug.Log("Type is not supported: " + type);
        return value;
    }

    private void ClearDictionary()
    {
        _Dictionary.Clear();
    }

    /// <summary>
    /// Add empty key/value pair to dictionary
    /// </summary>
    private void AddNewItem()
    {
        TK key;
        if (typeof(TK) == typeof(string))
            key = (TK)(object)"";
        else key = default(TK);

        var value = default(TV);
        try
        {
            _Dictionary.Add(key, value);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}

// bind property drawer to PrefabProviderInstance
[CustomPropertyDrawer(typeof(PrefabProviderInstance))]
public class PrefabProviderInstanceDrawer : DictionaryDrawer<string, GameObject> { }