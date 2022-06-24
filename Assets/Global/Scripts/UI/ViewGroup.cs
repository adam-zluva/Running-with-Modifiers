using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;

public class ViewGroup : MonoBehaviour
{
    [SerializeField] private View[] views;
    [SerializeField, ValueDropdown("viewIndexDropdown")] private View defaultView;
    private View activeView;
    private int activeViewIndex;

    private void Awake()
    {
        CloseAllViews();
        SetActiveView(defaultView);
    }

    public void CloseAllViews()
    {
        views.ForEach(view =>
        {
            view.Close();
        });
    }

    public void PreviousView()
    {
        int newIndex = (activeViewIndex - 1).Mod(views.Length);
        SetActiveView(newIndex);
    }

    public void NextView()
    {
        int newIndex = (activeViewIndex + 1).Mod(views.Length);
        SetActiveView(newIndex);
    }

    public void SetActiveView(int index)
    {
        if (activeView != null) activeView.Close();

        activeViewIndex = index;
        activeView = views[index];
        activeView.Open();
    }

    public void SetActiveView(View view)
    {
        for (int i = 0; i < views.Length; i++)
        {
            if (views[i] == view)
            {
                SetActiveView(i);
                return;
            }
        }
    }

    #region Editor
    private IEnumerable viewIndexDropdown = new ValueDropdownList<View>();

    void BuildDropdown()
    {
        if (views == null) return;

        var dropdownList = new ValueDropdownList<View>();
        for (int i = 0; i < views.Length; i++)
        {
            var view = views[i];

            if (view == null) continue;

            dropdownList.Add(view.viewObject.name, view);
        }

        viewIndexDropdown = dropdownList;
    }

    private void OnValidate()
    {
        BuildDropdown();
    }
    #endregion
}
