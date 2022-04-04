using System.Collections.Generic;
using UnityEngine;

//FIXME Currently there is no way to differentiate between different forms with the same ID
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

        // Always accept if new form for citizen
        if (!TransactionHandler.ListRequiredFormIds(false).Contains(id)) {
            return accepted;
        }

        Judgement[] allStamps = GetComponent<Stampable>().tfStampImageParent.GetComponentsInChildren<Judgement>();
        if (allStamps.Length == 0) {
            // If there are no stamps on this form, it is accepted if it was not just received by the citizen. Otherwise it must be judged
            //return !TransactionHandler.ListRequiredFormIds(false).Contains(id);

            return false;
        }
        accepted &= allStamps[allStamps.Length - 1].accepting; //Only check last stamp

        return accepted;
    }

    /// <summary>
    /// Try not to call this often, it's super unoptimized
    /// </summary>
    /// <returns>true if denied and all handed in forms are returned or accepted and all
    /// handed in and subsequently required forms are returned. Otherwise false.</returns>
    public bool IsReturnable() {
        bool returnable = true;

        List<string> requiredFormIds;
        if (IsAccepted()) {
            requiredFormIds = TransactionHandler.ListRequiredFormIds(true);
        } else {
            requiredFormIds = TransactionHandler.ListRequiredFormIds(false);
        }

        Stapleable stapleable = GetComponent<Stapleable>();
        if (stapleable != null) {
            List<Stapleable> allLinked = new List<Stapleable>();
            stapleable.GetAllLinked(allLinked);

            List<Form> allLinkedForms = new List<Form>();
            foreach (Stapleable s in allLinked) {
                Form f = s.GetComponent<Form>();
                allLinkedForms.Add(f);
            }
            allLinkedForms.Add(this); //We also need to add this form itself

            List<string> allLinkedFormIds = new List<string>(allLinkedForms.Count);
            foreach (Form f in allLinkedForms) {
                if (GetComponentsInChildren<Judgement>().Length == 0 && TransactionHandler.ListRequiredFormIds(false).Contains(id)) {
                    // Return false if form that requires approval or denial has neither
                    return false;
                }
                allLinkedFormIds.Add(f.id);
            }

            foreach (string requiredId in requiredFormIds) {
                returnable &= allLinkedFormIds.Contains(requiredId);
            }
        }

        return returnable;
    }

}
