using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Gui
{
    public class CategoryChooserScript : MonoBehaviour
    {

        public GameObject wordCategory;

        public int WordCategory
        {
            get
            {
                return int.Parse(wordCategory.GetComponent<Text>().text);
            }
            set
            {
                wordCategory.GetComponent<Text>().text = value.ToString();
            }
        }


        public void IncrementWordCategory()
        {
            WordCategory++;
        }

        public void DecrementWordCategory()
        {
            WordCategory--;
        }


    }
}
