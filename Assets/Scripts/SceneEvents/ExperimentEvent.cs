using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


// このクラスでさせること
// * スキル生成(スキル名入力 and ランダム)を行いgeneratedSkillにセット
// * 
public class ExperimentEvent : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera = null;
    [SerializeField] private Transform initTransform = null;
    [SerializeField] private Transform defaultTransform = null;

    // スキル名取得のみに使用
    [SerializeField] private TMP_InputField inputField = null;

    [SerializeField] private SkillGenerator skillGenerator = null;

    [SerializeField] private UI_EffectGenerator effectGenerator = null;
    [SerializeField] private Transform effectInstTransform = null;

    [SerializeField] private UI_Expt_DisplaySkill skillObjPrefab; // 生成した動的に表示させる
    [SerializeField] private Transform instantiateTransform = null;
    [SerializeField] private Transform instTransformAfterGenerated = null;
    [SerializeField] private GameObject eventCanvas = null;

    // インスペクタで確認のため[SerializeField]にしている
    [SerializeField] private UI_Expt_DisplaySkill displayedSkill = null;
    [SerializeField] private Skill generatedSkill = null; //生成されたスキル


    // スキル保存確認ようのパネル
    [SerializeField] UI_Expt_ConfirmPanel confirmPanel = null; //表示のみに使用

    private void Start()
    {
        // camera event
        StartCoroutine(FirstCameraMoving());
        skillGenerator.InitSkillGenerator();

        // Data load
        PlayerDataManager.instance.LoadPlayerData();
    }

    private IEnumerator FirstCameraMoving()
    {
        // イベント中はキャンバスは映さない
        eventCanvas.SetActive(false);

        mainCamera.transform.position = initTransform.position;
        mainCamera.transform.rotation = initTransform.rotation;
        mainCamera.transform.localScale = initTransform.localScale;

        float timer = 0f;
        float expectTime = 1f;
        while (timer < expectTime)
        {
            timer += Time.deltaTime;

            mainCamera.transform.position = Vector3.Lerp(initTransform.position, defaultTransform.position, timer / expectTime);
            mainCamera.transform.rotation = Quaternion.Lerp(initTransform.rotation, defaultTransform.rotation, timer / expectTime);
            mainCamera.transform.localScale = Vector3.Lerp(initTransform.localScale, defaultTransform.localScale, timer / expectTime);

            yield return null;
        }
        mainCamera.transform.position = defaultTransform.position;
        mainCamera.transform.rotation = defaultTransform.rotation;
        mainCamera.transform.localScale = defaultTransform.localScale;

        // カメラ移動後、キャンバスを復活
        eventCanvas.SetActive(true);
    }

    public Skill GetGeneratedSkill()
    {
        return generatedSkill;
    }

    // ボタンクリックで呼び出す
    public void StartGenerateSkill()
    {
        if (inputField.text.Trim().Length > 0)
        {
            StartCoroutine(GenerateSkill());
        }
        else
        {
            Debug.Log("inputField is empty string");
        }
    }

    // skillGeneratorからスキルを生成させ、generatedSkillにセット
    private IEnumerator GenerateSkill()
    {
        if (displayedSkill != null)
        {
            Destroy(displayedSkill.gameObject);
        }

        generatedSkill = null;
        if ((inputField.text != "" || inputField != null) && !skillGenerator.isGenerating)
        {
            skillGenerator.SetSkillName(inputField.text);
            Debug.Log("skill name: " + skillGenerator.GetSkillName());

            skillGenerator.GenerateSkill(); //スキル生成開始
            Vector3 instPos = effectInstTransform.position - 10 * new Vector3(0f, 1f, 0f);

            string[] attributes = { "cute", "cool", "unique" };
            string randomAtt = attributes[Random.Range(0, 3)];
            Debug.Log("randInt: " + randomAtt);
            effectGenerator.InstantiateEffects(instPos, randomAtt);
        }


        yield return new WaitUntil(() => !skillGenerator.isGenerating); //推論終了まで待つ

        Debug.Log("生成完了");
        generatedSkill = skillGenerator.GetGeneratedSkill();

        if (generatedSkill == null)
        {
            GenerateSkill();
        }

        if (generatedSkill != null)
        {
            InstantiateSkillObj();
        }

        // 生成完了後エフェクトをDestroy
        float scale = effectGenerator.GetMaxScale().x;
        float effectingTime = 1.0f;
        StartCoroutine(effectGenerator.DestroyEffects(scale, effectingTime));

    }


    // スキルobjを生成
    private void InstantiateSkillObj()
    {
        // これまでに生成したobjを削除
        if (displayedSkill != null)
        {
            Destroy(displayedSkill.gameObject);
        }

        // 動的生成
        displayedSkill = Instantiate(skillObjPrefab, instantiateTransform);
        displayedSkill.transform.position = instantiateTransform.position;

        displayedSkill.SetGeneratedSkill(generatedSkill);

        // キャンバスに表示させるため、eventCanvasを親オブジェクトにセット
        displayedSkill.transform.SetParent(eventCanvas.transform);

        //StartCoroutine(MoveSkillObj(instTransformAfterGenerated));
    }

    // 生成させたら、移動させる
    private IEnumerator MoveSkillObj(Transform transform)
    {
        // 生成後に呼ばれるが、1秒程度その場にとどまる
        yield return new WaitForSeconds(1.0f);

        // null check
        if (displayedSkill != null)
        {
            float moveTime = 1.0f;
            float timer = 0.0f;
            while (timer < moveTime)
            {
                timer += Time.deltaTime;

                displayedSkill.transform.position = Vector3.Lerp(instantiateTransform.position, transform.position, timer / moveTime);
                yield return null;
            }
        }
        
    }

    // 以前に同じスキルを生成したかどうかを確認し、同じものがあった場合、確認パネルを呼び出す
    private (bool, Skill) CheckSaveSkill()
    {
        // checking
        bool isSameSkill = false;
        Skill beforeSkill = null;
        if (generatedSkill != null && PlayerDataManager.instance.skillLibrary != null)
        {
            PlayerDataManager.instance.LoadSkillLibrary();
            foreach(Skill skill in PlayerDataManager.instance.skillLibrary.library)
            {
                // 同じものがヒットした時
                if (string.Equals(skill.skillName, generatedSkill.skillName))
                {
                    isSameSkill = true;
                    beforeSkill = skill;
                    break;
                }
            }
        }

        return (isSameSkill, beforeSkill);
    }

    

    // 生成したSkillLibraryに保存
    // スキル名が空白の場合は何もさせない
    public void SaveSkill()
    {
        Debug.Log("generatedSkill: " + generatedSkill.skillName);
        if (generatedSkill.skillName != "")
        {
            (bool, Skill) check = CheckSaveSkill();
            bool isSameSkill = check.Item1;
            Skill beforeSkill = check.Item2;

            Debug.Log("isSameSkill" + isSameSkill);

            // 同じものばあった場合
            if (isSameSkill)
            {
                confirmPanel.gameObject.SetActive(true);
                confirmPanel.PopUpMessage(beforeSkill, generatedSkill);
            }
            // ない場合はそのまま保存
            else
            {
                PlayerDataManager.instance.SaveSkillInSkillLibrary(generatedSkill);

                // inputFieldの内容を消す
                inputField.text = "";
                // 生成したskillPanelObjを消す
                Destroy(displayedSkill.gameObject);
            }

        }
        else
        {
            Debug.Log("skill is not generated");
        }

    }

    // 強制的に保存する
    public void SaveSkillDirectory()
    {
        if (generatedSkill != null)
        {
            PlayerDataManager.instance.LoadSkillLibrary();
            //foreach (Skill skill in PlayerDataManager.instance.skillLibrary.library)
            for (int i=0; i<PlayerDataManager.instance.skillLibrary.library.Count; i++)
            {
                // 同じものがヒットした時
                if (string.Equals(PlayerDataManager.instance.skillLibrary.library[i].skillName, generatedSkill.skillName))
                {
                    // ヒットしたスキルを削除
                    PlayerDataManager.instance.DeleteSkillInSkillLibrary(i);
                    // 代わりにgeneratedSkillを追加する
                    PlayerDataManager.instance.SaveSkillInSkillLibrary(generatedSkill);

                    Debug.Log($"generatedSkill: {generatedSkill.skillName},  {generatedSkill.parameters.cute},,  {generatedSkill.parameters.cool}, {generatedSkill.parameters.unique}");


                    // 追加したらパネルを決してreturnする
                    ClosePanel();
                    return;
                }
            }
            // 同じものがヒットしない場合はそのまま保存
            PlayerDataManager.instance.SaveSkillInSkillLibrary(generatedSkill);

        }
        ClosePanel();
        
    }

    // 確認パネルを閉じる
    public void ClosePanel()
    {
        confirmPanel.gameObject.SetActive(false);
    }

    public void PrintSkillLibrary()
    {
        PlayerDataManager.instance.PrintSkillLibrary();
    }


}
