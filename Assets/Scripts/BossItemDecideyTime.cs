using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class BossItemDecideyTime : MonoBehaviour
{
	public List<GameObject> itemPoolHead;
	public List<GameObject> itemPoolBody;
	public List<GameObject> itemPoolArm;
	public List<GameObject> itemPoolGun;

	public GameObject leftChoice;
	public GameObject rightChoice;
	private GameObject leftChoiceSelection;
	private GameObject rightChoiceSelection;
	public int currentLevel;
	private bool itemsSpawned;

	public void Update()
	{
		if (!itemsSpawned)
		{
			if (currentLevel == 0)
			{
				GameObject item1 = itemPoolHead[UnityEngine.Random.Range(0, itemPoolHead.Count - 1)];
				itemPoolBody.Remove(item1);
				GameObject item2 = itemPoolHead[UnityEngine.Random.Range(0, itemPoolHead.Count - 1)];
				itemPoolBody.Remove(item2);
				leftChoiceSelection = Instantiate(item1);
				leftChoiceSelection.transform.parent = leftChoice.transform;
				leftChoiceSelection.transform.localPosition = new Vector3();
				rightChoiceSelection = Instantiate(item2);
				rightChoiceSelection.transform.parent = rightChoice.transform;
				rightChoiceSelection.transform.localPosition = new Vector3();
			}
			else if (currentLevel == 1)
			{
				GameObject item1 = itemPoolGun[UnityEngine.Random.Range(0, itemPoolGun.Count - 1)];
				itemPoolBody.Remove(item1);
				GameObject item2 = itemPoolGun[UnityEngine.Random.Range(0, itemPoolGun.Count - 1)];
				itemPoolBody.Remove(item2);
				leftChoiceSelection = Instantiate(item1);
				leftChoiceSelection.transform.parent = leftChoice.transform;
				leftChoiceSelection.transform.localPosition = new Vector3();
				rightChoiceSelection = Instantiate(item2);
				rightChoiceSelection.transform.parent = rightChoice.transform;
				rightChoiceSelection.transform.localPosition = new Vector3();
			}
			else if (currentLevel == 2)
			{
				GameObject item1 = itemPoolBody[UnityEngine.Random.Range(0, itemPoolBody.Count - 1)];
				itemPoolBody.Remove(item1);
				GameObject item2 = itemPoolBody[UnityEngine.Random.Range(0, itemPoolBody.Count - 1)];
				itemPoolBody.Remove(item2);
				leftChoiceSelection = Instantiate(item1);
				leftChoiceSelection.transform.parent = leftChoice.transform;
				leftChoiceSelection.transform.localPosition = new Vector3();
				rightChoiceSelection = Instantiate(item2);
				rightChoiceSelection.transform.parent = rightChoice.transform;
				rightChoiceSelection.transform.localPosition = new Vector3();
			}
			else if (currentLevel == 3)
			{
				GameObject item1 = itemPoolArm[UnityEngine.Random.Range(0, itemPoolArm.Count - 1)];
				itemPoolBody.Remove(item1);
				GameObject item2 = itemPoolArm[UnityEngine.Random.Range(0, itemPoolArm.Count - 1)];
				itemPoolBody.Remove(item2);
				leftChoiceSelection = Instantiate(item1);
				leftChoiceSelection.transform.parent = leftChoice.transform;
				leftChoiceSelection.transform.localPosition = new Vector3();
				rightChoiceSelection = Instantiate(item2);
				rightChoiceSelection.transform.parent = rightChoice.transform;
				rightChoiceSelection.transform.localPosition = new Vector3();
			}

			itemsSpawned = true;
		}
		else
		{
			if (leftChoiceSelection == null)
			{
				Destroy(rightChoiceSelection);
				Destroy(this);
			}
			else if (rightChoiceSelection == null)
			{
				Destroy(leftChoiceSelection);
				Destroy(this);
			}
		}
	}
}