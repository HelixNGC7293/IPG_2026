using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GateController : MonoBehaviour, IMover
{
	public Vector3 Position
	{
		get
		{
			return transform.position;
		}
		set
		{
			transform.position = value;
		}
	}

	//Moving Speed
	[SerializeField]
	float gateSpeed = -10;

	//Display gate functions
	[SerializeField]
	TextMeshPro[] tX_Gates;

	//Only the first player passing through gate will the trigger the ability
	bool isGateTriggered = false;

	//Store current gate ability
	GameManager.Ability[] gateAbility;

	// Start is called before the first frame update
	void Start()
    {
        gateAbility = new GameManager.Ability[] { 
            new GameManager.Ability(Random.Range(0, 4), Random.Range(1, 10)), 
            new GameManager.Ability(Random.Range(0, 4), Random.Range(1, 10)) 
        };
        for (int i = 0; i < tX_Gates.Length; i++)
		{
            tX_Gates[i].text = GameManager.instance.abilityName[gateAbility[i].abilityID] + gateAbility[i].abilityPower;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (transform.position.z < -20)
		{
            Remove();
        }
    }

    public void Move()
    {
        transform.Translate(Vector3.forward * gateSpeed * Time.deltaTime);
    }

    public void Remove()
    {
        Destroy(gameObject);
    }

    public void GateTriggered(int gateID)
	{
        if (!isGateTriggered) {
            isGateTriggered = true;
            GameManager.instance.ApplyGateAbility(gateAbility[gateID]);
        }
    }
}
