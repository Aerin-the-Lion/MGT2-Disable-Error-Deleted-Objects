using HarmonyLib;
using UnityEngine;

namespace DisableErrorDeletedObjects
{

    public class DisableErrorDeletedObjects : MonoBehaviour
    {
        [HarmonyPrefix, HarmonyPatch(typeof(savegameScript), "LoadObjects")]
        static bool LoadGameWithDisableError(ES3Reader reader, string filename, savegameScript __instance, mainScript ___mS_, textScript ___tS_, sfxScript ___sfx_, mapScript ___mapS_)
        {
            Debug.Log("/////// -- LoadObjects -- ///////");
            int num = reader.Read<int>("anzObjects", -1);
            Debug.Log("anzObjects: " + num.ToString());
            if (num <= 0)
            {
                return false;
            }
            long[] array = new long[0];
            int[] array2 = new int[0];
            float[] array3 = new float[0];
            string[] array4 = new string[0];
            bool[] array5 = new bool[0];
            array2 = reader.Read<int[]>("objects_I");
            array3 = reader.Read<float[]>("objects_F");
            array5 = reader.Read<bool[]>("objects_B");
            int num2 = array2.Length / num;
            int num3 = array3.Length / num;
            int num4 = array5.Length / num;
            int num5 = array4.Length / num;
            int num6 = array.Length / num;
            Debug.Log("1./////// -- LoadObjects -- ///////");
            for (int i = 0; i < num; i++)
            {
                int num7 = i * num2;
                int num8 = i * num3;
                int num9 = i * num4;
                if (array2[num7] != 0 && !float.IsNaN(array3[num8]))
                {
                    Debug.Log("2./////// -- LoadObjects -- ///////");
                    GameObject gameObject = null;
                    try
                    {
                        if (array2[1 + num7] != -1)
                        {
                            Debug.Log("3.LoadInventarStart/////// -- LoadObjects -- ///////");
                            gameObject = UnityEngine.Object.Instantiate<GameObject>(___mapS_.prefabsInventar[array2[1 + num7]]);
                            Debug.Log("4.LoadInventarEnd/////// -- LoadObjects -- ///////");
                        }
                        if (array2[2 + num7] != -1)
                        {
                            gameObject = UnityEngine.Object.Instantiate<GameObject>(___mS_.miscGamePrefabs[array2[2 + num7]]);
                        }
                        Debug.Log("5./////// -- LoadObjects -- ///////");
                        gameObject.transform.position = new Vector3(array3[num8], 0f, array3[2 + num8]);
                        objectScript component = gameObject.GetComponent<objectScript>();
                        component.mS_ = ___mS_;
                        component.sfx_ = ___sfx_;
                        component.tS_ = ___tS_;
                        component.mapS_ = ___mapS_;
                        component.myID = array2[num7];
                        component.typ = array2[1 + num7];
                        component.typGhost = array2[2 + num7];
                        component.besetztCharID = array2[3 + num7];
                        component.aufladungenAkt = array2[4 + num7];
                        component.maschieneTimer = array3[4 + num8];
                        component.inUse = array5[num9];
                        component.InitObjectFromSavegame();
                        ___mS_.objectRotation = array3[3 + num8];
                        component.PlatziereObject(new Vector3(array3[num8], 0f, array3[2 + num8]), true, false, false, false);
                        component.ConsumeAufladung(0);
                        Debug.Log("6./////// -- LoadObjects -- ///////");
                    }
                    catch { }
                }
            }
            Debug.Log("End./////// -- LoadObjects -- ///////");
            return false;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(savegameScript), "LoadMitarbeiter")]
        static bool LoadMitarbeiterWithDisableError(ES3Reader reader, string filename, savegameScript __instance, mainScript ___mS_, textScript ___tS_, sfxScript ___sfx_, mapScript ___mapS_, createCharScript ___cCS_)
        {
			Debug.Log("/////// -- LoadMitarbeiter -- ///////");
			int num = reader.Read<int>("anzCharacter", -1);
			if (num <= 0)
			{
				return false;
			}
			//new long[0];
			int[] array = new int[0];
			float[] array2 = new float[0];
			string[] array3 = new string[0];
			bool[] array4 = new bool[0];
			bool[] array5 = new bool[0];
			array = reader.Read<int[]>("characters_I");
			array2 = reader.Read<float[]>("characters_F");
			array3 = reader.Read<string[]>("characters_S");
			array4 = reader.Read<bool[]>("characters_B");
			int num2 = array.Length / num;
			int num3 = array2.Length / num;
			int num4 = array4.Length / num;
			int num5 = array3.Length / num;
			if (___mS_.savegameVersion >= 16)
			{
				array5 = reader.Read<bool[]>("characters_perks");
			}
			for (int i = 0; i < num; i++)
			{
				int num6 = i * num2;
				int num7 = i * num3;
				int num8 = i * num4;
				int num9 = i * num5;
				characterScript characterScript = ___cCS_.CreateCharacter(array[num6], array4[num8], array[6 + num6]);
				characterScript.myID = array[num6];
				characterScript.group = array[1 + num6];
				characterScript.roomID = array[2 + num6];
				characterScript.objectUsingID = array[3 + num6];
				characterScript.objectBelegtID = array[4 + num6];
				characterScript.legend = array[5 + num6];
				characterScript.model_body = array[6 + num6];
				characterScript.model_eyes = array[7 + num6];
				characterScript.model_hair = array[8 + num6];
				characterScript.model_beard = array[9 + num6];
				characterScript.model_skinColor = array[10 + num6];
				characterScript.model_hairColor = array[11 + num6];
				characterScript.model_beardColor = array[12 + num6];
				characterScript.model_HoseColor = array[13 + num6];
				characterScript.model_ShirtColor = array[14 + num6];
				characterScript.model_Add1Color = array[15 + num6];
				characterScript.krank = array[16 + num6];
				characterScript.beruf = array[17 + num6];
				if (num2 > 18)
				{
					characterScript.gehalt = array[18 + num6];
				}
				characterScript.s_motivation = array2[3 + num7];
				characterScript.s_gamedesign = array2[4 + num7];
				characterScript.s_programmieren = array2[5 + num7];
				characterScript.s_grafik = array2[6 + num7];
				characterScript.s_sound = array2[7 + num7];
				characterScript.s_pr = array2[8 + num7];
				characterScript.s_gametests = array2[9 + num7];
				characterScript.s_technik = array2[10 + num7];
				characterScript.s_forschen = array2[11 + num7];
				characterScript.workProgress = array2[12 + num7];
				characterScript.durst = array2[13 + num7];
				characterScript.hunger = array2[14 + num7];
				characterScript.klo = array2[15 + num7];
				characterScript.waschbecken = array2[16 + num7];
				characterScript.muell = array2[17 + num7];
				characterScript.giessen = array2[18 + num7];
				characterScript.pause = array2[19 + num7];
				characterScript.freezer = array2[23 + num7];
				characterScript.myName = array3[num9];
				characterScript.male = array4[num8];
				characterScript.perks = new bool[array5.Length / num];
				int num10 = i * (array5.Length / num);
				for (int j = 0; j < characterScript.perks.Length; j++)
				{
					characterScript.perks[j] = array5[j + num10];
				}
				characterScript.gameObject.transform.GetChild(0).GetComponent<characterGFXScript>().Init(true);
				characterScript.gameObject.transform.position = new Vector3(array2[num7], array2[1 + num7], array2[2 + num7]);
				characterScript.gameObject.transform.eulerAngles = new Vector3(array2[20 + num7], array2[21 + num7], array2[22 + num7]);
				if (characterScript.objectBelegtID != -1)
				{
                try {
					characterScript.gameObject.GetComponent<movementScript>().FindObjectInRoom(-1, GameObject.Find("O_" + characterScript.objectBelegtID.ToString()), false);
                }catch { }
			}
			}
			return false;
		}
    }
}
