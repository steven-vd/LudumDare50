using System.Collections.Generic;
using UnityEngine;

public class Form : MonoBehaviour {

    public string id;
    public string[] formsToAddIfAccepted;

    public bool IsAccepted(bool checkRecursive = true) {
        bool accepted = true;

        if (checkRecursive) {
            Stapleable stapleable = GetComponent<Stapleable>();
            if (stapleable != null) {
                List<Stapleable> allLinked = new List<Stapleable>();
                stapleable.GetAllLinked(allLinked);
                foreach (Stapleable s in allLinked) {
                    accepted &= s.GetComponent<Form>().IsAccepted(false);
                }
            }
        }

        Judgement[] allStamps = GetComponent<Stampable>().tfStampImageParent.GetComponentsInChildren<Judgement>();
        if (allStamps.Length == 0) {
            // If there are no stamps on this form, it is accepted if it was not just received by the citizen. Otherwise it must be judged
            return !TransactionHandler.ListRequiredFormIds(false).Contains(id);
        }
        accepted &= allStamps[allStamps.Length - 1].accepting;

        return accepted;
    }

}
