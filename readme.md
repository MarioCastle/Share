![image](https://user-images.githubusercontent.com/29988144/128678318-4ba40a87-d7f0-4f37-a903-d8ec7dcf458c.png)
![image](https://user-images.githubusercontent.com/29988144/128678547-a1cc2322-cc3b-4e37-9643-1990701ea225.png)

![image](https://user-images.githubusercontent.com/29988144/128678402-278f7797-4c6f-42a6-a7be-d2e7e6fa62d8.png)




```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace  LP.FDG.InputManager
{  
    public class InputHandler : MonoBehaviour
    {
        public static InputHandler instance;
        private RaycastHit hit; //what we hit with our ray

        private List<Transform> selectedUnits = new List<Transform>();
        // Start is called before the first frame update
        void Start()
        {
            instance = this;
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void HandleUnitMovement()
        {
            if(Input.GetMouseButtonDown(1))
            {
                Debug.Log("Right Clicked");
            }
            
            if(Input.GetMouseButtonDown(0)) // 0 left 1 right click
            {
                Debug.Log("Clicked");
                //Create a Ray
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                // Check if we hit somthing
                if (Physics.Raycast(ray,out hit))
                {
                    //if we do , then do something with that data
                    LayerMask layerHit = hit.transform.gameObject.layer;

                    switch (layerHit.value)
                    {
                        case 8: // Units Layer
                            //do something
                            // click leftShift to MultiSelect
                            //if it down(Click) We 're gonna send true ,if not be false
                            SelectUnit(hit.transform);
                                break;
                            default: //if none of the above happens
                                //do something
                                // DeselectUnits();
                                break;
                    }
                }
            }

        }
        private void SelectUnit(Transform unit)
        {
            // if(!canMultiselect)//if we cant multiSelect
            // {
            //    DeselectUnits();  //Deselect first before select unit
            // }

            selectedUnits.Add(unit);
            //lets set an obj on the unit called Highlight
            unit.Find("Highlight").gameObject.SetActive(true);
        }
    }

}

```
