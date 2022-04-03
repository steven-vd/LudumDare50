using System.Collections.Generic;
using UnityEngine;

public class TransactionHandler : MonoBehaviour {

    public static TransactionHandler Instance;

    public GameObject goPackage;

    private void Awake() {
        Instance = this;
    }

    public static List<string> ListRequiredFormIds(bool withFormsToAddIfAccepted) {
        List<string> requiredFormIds = new List<string>();
        Form[] directForms = Instance.goPackage.GetComponentsInChildren<Form>();
        foreach (Form f in directForms) {
            requiredFormIds.Add(f.id);
            if (withFormsToAddIfAccepted) {
                requiredFormIds.AddRange(f.formsToAddIfAccepted);
            }
        }
        return requiredFormIds;
    }

}
