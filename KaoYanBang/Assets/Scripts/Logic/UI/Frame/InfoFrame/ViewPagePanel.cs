using liulaoc.UI.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewPagePanel : UIPanel
{
    #region 参数设置
    [SerializeField] private float showTimeInterval = 2.0f;
    [SerializeField] private float moveSpeedTime = 1.0f;
    #endregion
    #region view
    private RectTransform showTrans;//显示区域
    private Button lastBtn;
    private Button nextBtn;
    private RectTransform leftPageRec;
    private RectTransform rightPageRec;
    #endregion
    #region model
    private int nowImgIndex = 0;
    private List<ViewPagePrefab> allPages = new List<ViewPagePrefab>();
    private float timer = 0;//用于计时轮播图播放间隔
    private bool isMove = false;
    #endregion
    #region prefabResource
    #endregion


    protected override void AddListener()
    {
        //显示上一个轮播图
        lastBtn.onClick.AddListener(ShowLastPage);
        nextBtn.onClick.AddListener(ShowNextPage);
        //显示下一个轮播图
    }

    protected override void BindView()
    {
        showTrans = GetComponent<RectTransform>();
        leftPageRec = transform.Find("LeftPage").GetComponent<RectTransform>();
        rightPageRec = transform.Find("RightPage").GetComponent<RectTransform>();
        lastBtn = transform.Find("LastBtn").GetComponent<Button>();
        nextBtn = transform.Find("NextBtn").GetComponent<Button>();
    }
    private void LateUpdate()
    {
    }
    private void UpdateView()
    {

    }
    private void OnEnable()
    {
        //获取加载所有轮播图
        //初始化栈数据
        //开始播放
    }
    private void OnDisable()
    {
        //删除所有轮播图
        var n = allPages.Count;
        for(int i=0;i<n;i++)
        {
            Destroy(allPages[i].gameObject);
        }
        allPages.Clear();
    }
    /// <summary>
    /// 播放轮播图
    /// </summary>
    /// <returns></returns>
    private IEnumerator CirclePlayViewPageAsync()
    {
        while(allPages != null)
        {
            while (timer <= showTimeInterval)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            //计时结束，移动页面
            ShowNextPage();
            yield return null;
        }


    }
    private void ShowNextPage()
    {
        if(isMove)
        {
            if (isMove)
            {
                return;
            }
        }
        var nowPage = allPages[nowImgIndex];
        nowImgIndex = (nowImgIndex + 1) % allPages.Count;
        var nextPages = allPages[nowImgIndex];
        MovePage(nowPage,leftPageRec);
        MovePage(nextPages,showTrans);
    }
    private void ShowLastPage()
    {
        if (isMove)
        {
            return;
        }
        var nowPage = allPages[nowImgIndex];
        nowImgIndex--;
        if(nowImgIndex < 0)
        {
            nowImgIndex = allPages.Count - 1 + nowImgIndex;
        }
        var nextPages = allPages[nowImgIndex];
        MovePage(nowPage, rightPageRec);
        MovePage(nextPages, showTrans);
    }
    private IEnumerator MovePage(ViewPagePrefab page,RectTransform target)
    {
        isMove = true;
        var timer = 0f;
        var rec = page.GetComponent<RectTransform>();
        Vector2 startPos = rec.position;
        while (timer <= moveSpeedTime)
        {
            timer += Time.deltaTime;
            float t = timer / moveSpeedTime;
            Vector2 pos = Vector2.Lerp(startPos, target.position, t);
            rec.position = pos;
            yield return null;
        }
        isMove = false;
    }
}
