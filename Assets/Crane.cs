using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Crane : MonoBehaviour
{
    [SerializeField] private GameObject Slider1, Slider2, ArmRotate, Arm;
    [SerializeField] private GameObject Target, SubTarget;
    [SerializeField] private UIDocument document;
    [SerializeField] private float d3, d4;
    
    private Slider slider1Slider, slider2Slider, armRotateSlider, armSlider;
    
    Vector3 calculatePositionSub(float d1, float d2, float d3, float d4, float theta1, float theta2)
    {
        var position = new Vector3();
        position.x = d2;
        position.y = d1;
        position.z = -d3 + 0.010f;
        return position;
    }
    Vector3 calculatePosition(float d1, float d2, float d3, float d4, float theta1, float theta2)
    {
        var position = new Vector3();
        position.x = d2 - Mathf.Cos(theta1) * Mathf.Sin(theta2) * d4;
        position.y = d1 - Mathf.Sin(theta1) * Mathf.Sin(theta2) * d4;
        position.z = -d3 - Mathf.Cos(theta2) * d4 + 0.010f;
        return position;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        slider1Slider = document.rootVisualElement.Q<Slider>("Slider1");
        slider1Slider.RegisterValueChangedCallback(evt =>
        {
            var position = Slider1.transform.localPosition;
            position.y = evt.newValue;
            Slider1.transform.localPosition = position;
            Debug.Log(evt.newValue);
        });
        slider2Slider = document.rootVisualElement.Q<Slider>("Slider2");
        slider2Slider.RegisterValueChangedCallback(evt =>
        {
            var position = Slider2.transform.localPosition;
            position.x = evt.newValue;
            Slider2.transform.localPosition = position;
            Debug.Log(evt.newValue);
        });
        armRotateSlider = document.rootVisualElement.Q<Slider>("ArmRotate");
        armRotateSlider.RegisterValueChangedCallback(evt =>
        {
            var rotation = ArmRotate.transform.localEulerAngles;
            rotation.z = evt.newValue;
            ArmRotate.transform.localEulerAngles = rotation;
            Debug.Log(evt.newValue);
        });
        armSlider = document.rootVisualElement.Q<Slider>("Arm");
        armSlider.RegisterValueChangedCallback(evt =>
        {
            var rotation = Arm.transform.localEulerAngles;
            rotation.y = evt.newValue;
            Arm.transform.localEulerAngles = rotation;
            Debug.Log("Rotation" + evt.newValue);
        });
    }

    // Update is called once per frame
    void Update()
    {
        SubTarget.transform.localPosition = calculatePositionSub(slider1Slider.value, slider2Slider.value, d3, d4, Mathf.Deg2Rad * armRotateSlider.value, Mathf.Deg2Rad * armSlider.value);
        Target.transform.localPosition = calculatePosition(slider1Slider.value, slider2Slider.value, d3, d4, Mathf.Deg2Rad * armRotateSlider.value, Mathf.Deg2Rad * armSlider.value);
    }
}
