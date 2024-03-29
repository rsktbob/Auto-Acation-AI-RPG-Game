using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using System;
using Unity.Mathematics;
using UnityEditor;

public class FightmanAgent : Agent
{
    [SerializeField] private GameObject robotPrefab;
    [SerializeField] private float rewardUpperLimit;
    [SerializeField] private GameObject target;
    private Fightman robot;

    // reset the agent or remove it from the environment
    public override void OnEpisodeBegin()
    {
        CreateNewRobot();
        InvokeRepeating("AddRewardToRobot", 1f, 1f); // add reward every 1 sec
        InvokeRepeating("JudgeWhetherEnterNextEpisode", 5f, 5f); // judge whether fall down every 5 sec
        InvokeRepeating("JudgeWhetherEnterNextEpisode2", 20f, 20f); // judge next episode every 20 sec
    }

    // to specify agent behavior at every step, based on the provided action
    public override void OnActionReceived(ActionBuffers actions)
    {

        /* Left Leg, Foot*/
        Vector3 targetLeftUpLegAngularVelocity = new Vector3(
            actions.ContinuousActions[0],
            actions.ContinuousActions[1],
            actions.ContinuousActions[2]
            );
        Vector3 targetLeftLegAngularVelocity = new Vector3(
            actions.ContinuousActions[3],
            actions.ContinuousActions[4],
            actions.ContinuousActions[5]
            );
        Vector3 targetLeftFootAngularVelocity = new Vector3(
            actions.ContinuousActions[6],
            actions.ContinuousActions[7],
            actions.ContinuousActions[8]
            );

        /* Right leg, Foot */
        Vector3 targetRightUpLegAngularVelocity = new Vector3(
            actions.ContinuousActions[9],
            actions.ContinuousActions[10],
            actions.ContinuousActions[11]
            );
        Vector3 targetRightLegAngularVelocity = new Vector3(
            actions.ContinuousActions[12],
            actions.ContinuousActions[13],
            actions.ContinuousActions[14]
            );
        Vector3 targetRightFootAngularVelocity = new Vector3(
            actions.ContinuousActions[15],
            actions.ContinuousActions[16],
            actions.ContinuousActions[17]
            );


        /* Left shoulder, arm, hand */
        Vector3 targetLeftShoulderAngularVelocity = new Vector3(
            actions.ContinuousActions[18],
            actions.ContinuousActions[19],
            actions.ContinuousActions[20]
            );
        Vector3 targetLeftArmAngularVelocity = new Vector3(
            actions.ContinuousActions[21],
            actions.ContinuousActions[22],
            actions.ContinuousActions[23]
            );
        Vector3 targetLeftHandAngularVelocity = new Vector3(
            actions.ContinuousActions[24],
            actions.ContinuousActions[25],
            actions.ContinuousActions[26]
            );

        /* Right shoulder, arm, hand */
        Vector3 targetRightShoulderAngularVelocity = new Vector3(
            actions.ContinuousActions[27],
            actions.ContinuousActions[28],
            actions.ContinuousActions[29]
            );
        Vector3 targetRightArmAngularVelocity = new Vector3(
            actions.ContinuousActions[30],
            actions.ContinuousActions[31],
            actions.ContinuousActions[32]
            );
        Vector3 targetRightHandAngularVelocity = new Vector3(
            actions.ContinuousActions[33],
            actions.ContinuousActions[34],
            actions.ContinuousActions[35]
            );
        /* Hips */
        Vector3 targetHipAngularVelocity = new Vector3(
            actions.ContinuousActions[36],
            actions.ContinuousActions[37],
            actions.ContinuousActions[38]
            );

        /* Chest and Waist and Head */
        Vector3 targetWaistAngularVelocity = new Vector3(
            actions.ContinuousActions[39],
            actions.ContinuousActions[40],
            actions.ContinuousActions[41]
            );

        Vector3 targetHeadAngularVelocity = new Vector3(
            actions.ContinuousActions[42],
            actions.ContinuousActions[43],
            actions.ContinuousActions[44]
            );

        // output Left Leg, Foot
        robot.LeftLeg.AddAngularVelocity(10, 3, 10, targetLeftLegAngularVelocity);
        robot.LeftUpLeg.AddAngularVelocity(10, 3, 10, targetLeftUpLegAngularVelocity);
        robot.LeftFoot.AddAngularVelocity(10, 3, 10, targetLeftFootAngularVelocity);

        // output right Leg, Foot
        robot.RightLeg.AddAngularVelocity(10, 3, 10, targetRightLegAngularVelocity);
        robot.RightUpLeg.AddAngularVelocity(10, 3, 10, targetRightUpLegAngularVelocity);
        robot.RightFoot.AddAngularVelocity(10, 3, 10, targetRightFootAngularVelocity);

        // output Left shoulder, arm, hand
        robot.LeftShoulder.AddAngularVelocity(5, 2, 5, targetLeftShoulderAngularVelocity);
        robot.LeftArm.AddAngularVelocity(5, 2, 5, targetLeftArmAngularVelocity);
        robot.LeftHand.AddAngularVelocity(5, 2, 5, targetLeftHandAngularVelocity);

        // output Right shoulder, arm, hand
        robot.RightShoulder.AddAngularVelocity(5, 2, 5, targetRightShoulderAngularVelocity);
        robot.RightArm.AddAngularVelocity(5, 2, 5, targetRightArmAngularVelocity);
        robot.RightHand.AddAngularVelocity(5, 2, 5, targetRightHandAngularVelocity);

        robot.Hips.AddAngularVelocity(5, 2, 5, targetHipAngularVelocity);

        robot.Waist.AddAngularVelocity(5, 5, 5, targetWaistAngularVelocity);
        robot.Chest.AddAngularVelocity(5, 5, 5, targetWaistAngularVelocity);

        robot.Head.AddAngularVelocity(3, 3, 3, targetHeadAngularVelocity);

        //// output Left Leg, Foot
        //robot.LeftLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity = new Vector3(Mathf.Clamp(robot.LeftLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity.x + targetLeftLegAngularVelocity.x, -10f, 10f), Mathf.Clamp(robot.LeftLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity.y + targetLeftLegAngularVelocity.y, -3f, 3f), Mathf.Clamp(robot.LeftLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity.z + targetLeftLegAngularVelocity.z, -10f, 10f));
        //robot.LeftUpLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity = new Vector3(Mathf.Clamp(robot.LeftUpLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity.x + targetLeftUpLegAngularVelocity.x, -10f, 10f), Mathf.Clamp(robot.LeftUpLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity.y + targetLeftUpLegAngularVelocity.y, -3f, 3f), Mathf.Clamp(robot.LeftUpLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity.z + targetLeftUpLegAngularVelocity.z, -10f, 10f)); ;
        //robot.LeftFoot.GetComponent<ConfigurableJoint>().targetAngularVelocity = new Vector3(Mathf.Clamp(robot.LeftFoot.GetComponent<ConfigurableJoint>().targetAngularVelocity.x + targetLeftFootAngularVelocity.x, -10f, 10f), Mathf.Clamp(robot.LeftFoot.GetComponent<ConfigurableJoint>().targetAngularVelocity.y + targetLeftFootAngularVelocity.y, -3f, 3f), Mathf.Clamp(robot.LeftFoot.GetComponent<ConfigurableJoint>().targetAngularVelocity.z + targetLeftFootAngularVelocity.z, -10f, 10f)); ;

        //// output right Leg, Foot
        //robot.RightLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity = new Vector3(Mathf.Clamp(robot.RightLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity.x + targetRightLegAngularVelocity.x, -10f, 10f), Mathf.Clamp(robot.RightLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity.y + targetRightLegAngularVelocity.y, -3f, 3f), Mathf.Clamp(robot.RightLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity.z + targetRightLegAngularVelocity.z, -10f, 10f)); ;
        //robot.RightUpLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity = new Vector3(Mathf.Clamp(robot.RightUpLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity.x + targetRightUpLegAngularVelocity.x, -10f, 10f), Mathf.Clamp(robot.RightUpLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity.y + targetRightUpLegAngularVelocity.y, -3f, 3f), Mathf.Clamp(robot.RightUpLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity.z + targetRightUpLegAngularVelocity.z, -10f, 10f)); ;
        //robot.RightFoot.GetComponent<ConfigurableJoint>().targetAngularVelocity = new Vector3(Mathf.Clamp(robot.RightFoot.GetComponent<ConfigurableJoint>().targetAngularVelocity.x + targetRightFootAngularVelocity.x, -10f, 10f), Mathf.Clamp(robot.RightFoot.GetComponent<ConfigurableJoint>().targetAngularVelocity.y + targetRightFootAngularVelocity.y, -3f, 3f), Mathf.Clamp(robot.RightFoot.GetComponent<ConfigurableJoint>().targetAngularVelocity.z + targetRightFootAngularVelocity.z, -10f, 10f)); ;

        //// output Left shoulder, arm, hand
        //robot.LeftShoulder.GetComponent<ConfigurableJoint>().targetAngularVelocity = new Vector3(Mathf.Clamp(robot.LeftShoulder.GetComponent<ConfigurableJoint>().targetAngularVelocity.x + targetLeftShoulderAngularVelocity.x, -5f, 5f), Mathf.Clamp(robot.LeftShoulder.GetComponent<ConfigurableJoint>().targetAngularVelocity.y + targetLeftShoulderAngularVelocity.y, -2f, 2f), Mathf.Clamp(robot.LeftShoulder.GetComponent<ConfigurableJoint>().targetAngularVelocity.z + targetLeftShoulderAngularVelocity.z, -5f, 5f)); ;
        //robot.LeftArm.GetComponent<ConfigurableJoint>().targetAngularVelocity = new Vector3(Mathf.Clamp(robot.LeftArm.GetComponent<ConfigurableJoint>().targetAngularVelocity.x + targetLeftArmAngularVelocity.x, -5f, 5f), Mathf.Clamp(robot.LeftArm.GetComponent<ConfigurableJoint>().targetAngularVelocity.y + targetLeftArmAngularVelocity.y, -2f, 2f), Mathf.Clamp(robot.LeftArm.GetComponent<ConfigurableJoint>().targetAngularVelocity.z + targetLeftArmAngularVelocity.z, -5f, 5f)); ;
        //robot.LeftHand.GetComponent<ConfigurableJoint>().targetAngularVelocity = new Vector3(Mathf.Clamp(robot.LeftHand.GetComponent<ConfigurableJoint>().targetAngularVelocity.x + targetLeftHandAngularVelocity.x, -5f, 5f), Mathf.Clamp(robot.LeftHand.GetComponent<ConfigurableJoint>().targetAngularVelocity.y + targetLeftHandAngularVelocity.y, -2f, 2f), Mathf.Clamp(robot.LeftHand.GetComponent<ConfigurableJoint>().targetAngularVelocity.z + targetLeftHandAngularVelocity.z, -5f, 5f)); ;

        //// output Right shoulder, arm, hand
        //robot.RightShoulder.GetComponent<ConfigurableJoint>().targetAngularVelocity = new Vector3(Mathf.Clamp(robot.RightShoulder.GetComponent<ConfigurableJoint>().targetAngularVelocity.x + targetRightShoulderAngularVelocity.x, -5f, 5f), Mathf.Clamp(robot.RightShoulder.GetComponent<ConfigurableJoint>().targetAngularVelocity.y + targetRightShoulderAngularVelocity.y, -2f, 2f), Mathf.Clamp(robot.RightShoulder.GetComponent<ConfigurableJoint>().targetAngularVelocity.z + targetRightShoulderAngularVelocity.z, -5f, 5f)); ;
        //robot.RightArm.GetComponent<ConfigurableJoint>().targetAngularVelocity = new Vector3(Mathf.Clamp(robot.RightArm.GetComponent<ConfigurableJoint>().targetAngularVelocity.x + targetRightArmAngularVelocity.x, -5f, 5f), Mathf.Clamp(robot.RightArm.GetComponent<ConfigurableJoint>().targetAngularVelocity.y + targetRightArmAngularVelocity.y, -2f, 2f), Mathf.Clamp(robot.RightArm.GetComponent<ConfigurableJoint>().targetAngularVelocity.z + targetRightArmAngularVelocity.z, -5f, 5f)); ;
        //robot.RightHand.GetComponent<ConfigurableJoint>().targetAngularVelocity = new Vector3(Mathf.Clamp(robot.RightHand.GetComponent<ConfigurableJoint>().targetAngularVelocity.x + targetRightHandAngularVelocity.x, -5f, 5f), Mathf.Clamp(robot.RightHand.GetComponent<ConfigurableJoint>().targetAngularVelocity.y + targetRightHandAngularVelocity.y, -2f, 2f), Mathf.Clamp(robot.RightHand.GetComponent<ConfigurableJoint>().targetAngularVelocity.z + targetRightHandAngularVelocity.z, -5f, 5f)); ;

        //robot.Hips.GetComponent<ConfigurableJoint>().targetAngularVelocity = new Vector3(Mathf.Clamp(robot.Hips.GetComponent<ConfigurableJoint>().targetAngularVelocity.x + targetHipAngularVelocity.x, -5f, 5f), Mathf.Clamp(robot.Hips.GetComponent<ConfigurableJoint>().targetAngularVelocity.y + targetHipAngularVelocity.y, -2f, 2f), Mathf.Clamp(robot.Hips.GetComponent<ConfigurableJoint>().targetAngularVelocity.z + targetHipAngularVelocity.z, -5f, 5f)); ;

        //robot.Waist.GetComponent<ConfigurableJoint>().targetAngularVelocity = new Vector3(Mathf.Clamp(robot.Waist.GetComponent<ConfigurableJoint>().targetAngularVelocity.x + targetWaistAngularVelocity.x, -5f, 5f), Mathf.Clamp(robot.Waist.GetComponent<ConfigurableJoint>().targetAngularVelocity.y + targetWaistAngularVelocity.y, -5f, 5f), Mathf.Clamp(robot.Waist.GetComponent<ConfigurableJoint>().targetAngularVelocity.z + targetWaistAngularVelocity.z, -5f, 5f)); ;
        //robot.Chest.GetComponent<ConfigurableJoint>().targetAngularVelocity = new Vector3(Mathf.Clamp(robot.Chest.GetComponent<ConfigurableJoint>().targetAngularVelocity.x + targetWaistAngularVelocity.x, -5f, 5f), Mathf.Clamp(robot.Chest.GetComponent<ConfigurableJoint>().targetAngularVelocity.y + targetWaistAngularVelocity.y, -5f, 5f), Mathf.Clamp(robot.Chest.GetComponent<ConfigurableJoint>().targetAngularVelocity.z + targetWaistAngularVelocity.z, -5f, 5f)); ;

        //robot.Head.GetComponent<ConfigurableJoint>().targetAngularVelocity = new Vector3(Mathf.Clamp(robot.Head.GetComponent<ConfigurableJoint>().targetAngularVelocity.x + targetHeadAngularVelocity.x, -3f, 3f), Mathf.Clamp(robot.Head.GetComponent<ConfigurableJoint>().targetAngularVelocity.y + targetHeadAngularVelocity.y, -3f, 3f), Mathf.Clamp(robot.Head.GetComponent<ConfigurableJoint>().targetAngularVelocity.z + targetHeadAngularVelocity.z, -3f, 3f));
    }

    // to collect the vector observations of the agent for the step.(input)
    public override void CollectObservations(VectorSensor sensor)
    {
        /* Left Up Leg */
        sensor.AddObservation(RelativeBodyPosition(robot.LeftUpLeg.transform.position)); // position
        sensor.AddObservation(robot.LeftUpLeg.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.LeftUpLeg.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        /* Left Leg */
        sensor.AddObservation(RelativeBodyPosition(robot.LeftLeg.transform.position)); // position
        sensor.AddObservation(robot.LeftLeg.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.LeftLeg.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        /* Left Foot */
        sensor.AddObservation(RelativeBodyPosition(robot.LeftFoot.transform.position)); // position
        sensor.AddObservation(robot.LeftFoot.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.LeftFoot.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        /* Right Up Leg */
        sensor.AddObservation(RelativeBodyPosition(robot.RightUpLeg.transform.position)); // position
        sensor.AddObservation(robot.RightUpLeg.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.RightUpLeg.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        /* Right Leg */
        sensor.AddObservation(RelativeBodyPosition(robot.RightLeg.transform.position)); // position
        sensor.AddObservation(robot.RightLeg.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.RightLeg.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        /* Right Foot */
        sensor.AddObservation(RelativeBodyPosition(robot.RightFoot.transform.position)); // position
        sensor.AddObservation(robot.RightFoot.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.RightFoot.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        // -----------------------------------
        /* Left Shoulder */
        sensor.AddObservation(RelativeBodyPosition(robot.LeftShoulder.transform.position)); // position
        sensor.AddObservation(robot.LeftShoulder.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.LeftShoulder.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        /* Left Arm */
        sensor.AddObservation(RelativeBodyPosition(robot.LeftArm.transform.position)); // position
        sensor.AddObservation(robot.LeftArm.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.LeftArm.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        /* Left Hand */
        sensor.AddObservation(RelativeBodyPosition(robot.LeftHand.transform.position)); // position
        sensor.AddObservation(robot.LeftHand.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.LeftHand.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        /* Right Shoulder */
        sensor.AddObservation(RelativeBodyPosition(robot.RightShoulder.transform.position)); // position
        sensor.AddObservation(robot.RightShoulder.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.RightShoulder.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        /* Right Arm */
        sensor.AddObservation(RelativeBodyPosition(robot.RightArm.transform.position)); // position
        sensor.AddObservation(robot.RightArm.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.RightArm.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        /* Right Hand */
        sensor.AddObservation(RelativeBodyPosition(robot.RightHand.transform.position)); // position
        sensor.AddObservation(robot.RightHand.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.RightHand.GetComponent<Rigidbody>().angularVelocity); // angular velocity


        // -----------------------------------

        /* Hip */
        sensor.AddObservation(robot.Hips.transform.localPosition); // position
        sensor.AddObservation(robot.Hips.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.Hips.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        // -----------------------------------

        /* Chest */
        sensor.AddObservation(RelativeBodyPosition(robot.Chest.transform.position)); // position
        sensor.AddObservation(robot.Chest.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.Chest.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        /* Waist */
        sensor.AddObservation(RelativeBodyPosition(robot.Waist.transform.position)); // position
        sensor.AddObservation(robot.Waist.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.Waist.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        /* Head */
        sensor.AddObservation(RelativeBodyPosition(robot.Head.transform.position)); // position
        sensor.AddObservation(robot.Head.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.Head.GetComponent<Rigidbody>().angularVelocity); // angular velocity

        /* Sword */
        sensor.AddObservation(RelativeBodyPosition(robot.Sword.transform.position)); // position
        sensor.AddObservation(robot.Sword.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.Sword.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        sensor.AddObservation(robot.Sword.transform.rotation);

        ////is touch floor
        sensor.AddObservation(robot.LeftUpLeg.isTouchFloor);
        sensor.AddObservation(robot.LeftLeg.isTouchFloor);
        sensor.AddObservation(robot.LeftFoot.isTouchFloor);
        sensor.AddObservation(robot.RightUpLeg.isTouchFloor);
        sensor.AddObservation(robot.RightLeg.isTouchFloor);
        sensor.AddObservation(robot.RightFoot.isTouchFloor);
        sensor.AddObservation(robot.LeftShoulder.isTouchFloor);
        sensor.AddObservation(robot.RightArm.isTouchFloor);
        sensor.AddObservation(robot.RightHand.isTouchFloor);
        sensor.AddObservation(robot.Hips.isTouchFloor);
        sensor.AddObservation(robot.Chest.isTouchFloor);
        sensor.AddObservation(robot.Waist.isTouchFloor);
        sensor.AddObservation(robot.Head.isTouchFloor);
        sensor.AddObservation(robot.Sword.isInBody);
    }

    //Add reward to robot
    private void AddRewardToRobot() 
    {
        AddReward(robot.Hips.transform.localPosition.y > 2.2f ? 1 : -1);
        AddReward(robot.Head.transform.localPosition.y > 2.6f ? 1 : -1);
        //AddReward(robot.Hips.transform.localPosition.y - 0.5f);
        //AddReward(robot.Chest.transform.localPosition.y - 0.5f);

        float eulerChestY = robot.Chest.transform.eulerAngles.y < 180 ? robot.Chest.transform.eulerAngles.y : 360 - robot.Chest.transform.eulerAngles.y;
        AddReward((45f - eulerChestY) /70f);

        float eulerHipX = robot.Hips.transform.eulerAngles.x < 180 ? robot.Hips.transform.eulerAngles.x : 360 - robot.Hips.transform.eulerAngles.x;
        float eulerHipsZ = robot.Hips.transform.eulerAngles.z < 180 ? robot.Hips.transform.eulerAngles.z : 360 - robot.Hips.transform.eulerAngles.z;
        AddReward((45 - eulerHipX - eulerHipsZ) / 100);

        float eulerChestX = robot.Chest.transform.eulerAngles.x < 180 ? robot.Chest.transform.eulerAngles.x : 360 - robot.Chest.transform.eulerAngles.x;
        float eulerChestZ = robot.Chest.transform.eulerAngles.z < 180 ? robot.Chest.transform.eulerAngles.z : 360 - robot.Chest.transform.eulerAngles.z;
        AddReward((45 - eulerChestX - eulerChestZ) / 100);

        float eulerLeftFootX = robot.LeftFoot.transform.eulerAngles.x < 180 ? robot.LeftFoot.transform.eulerAngles.x : 360 - robot.LeftFoot.transform.eulerAngles.x;
        float eulerLeftFootZ = robot.LeftFoot.transform.eulerAngles.z < 180 ? robot.LeftFoot.transform.eulerAngles.z : 360 - robot.LeftFoot.transform.eulerAngles.z;
        AddReward((45 - eulerLeftFootX - eulerLeftFootZ) / 100);

        float eulerRightFootX = robot.RightFoot.transform.eulerAngles.x < 180 ? robot.RightFoot.transform.eulerAngles.x : 360 - robot.RightFoot.transform.eulerAngles.x;
        float eulerRightFootZ = robot.RightFoot.transform.eulerAngles.z < 180 ? robot.RightFoot.transform.eulerAngles.z : 360 - robot.RightFoot.transform.eulerAngles.z;
        AddReward((45 - eulerRightFootX - eulerRightFootZ) / 100);

        //if(robot.Chest.transform.localPosition.y > 2.5)
        //    AddReward(4 - Vector3.Distance(robot.Sword.transform.position, target.transform.position));
    }



    public void Heuristic(float[] actionsOut)
    {

    }

    private void CreateNewRobot()
    {
        if (robot != null)
        {
            Destroy(robot.gameObject);
        }
        robot = Instantiate(robotPrefab, new Vector3(transform.position.x + 1.3f, transform.position.y - 1f, transform.position.z), new Quaternion(0,0,0,0)).GetComponent<Fightman>();
        robot.transform.eulerAngles = new Vector3(0, 90f, 0);
        robot.transform.parent = transform;
        robot.RewardEvent.AddListener(AddReward);
    }

    private Vector3 RelativeBodyPosition(Vector3 position)
    {
        return position - robot.Hips.transform.position;
    }

    private Vector3 RelativeFloorPosition(Vector3 position)
    {
        return position - transform.position;
    }

    // Judge whether enter next episode
    
    private void JudgeWhetherEnterNextEpisode()
    {
        float actualPositionY = robot.Head.transform.localPosition.y;
        if (actualPositionY < rewardUpperLimit)
        {
            CancelInvoke("AddRewardToRobot");
            CancelInvoke("JudgeWhetherEnterNextEpisode");
            CancelInvoke("JudgeWhetherEnterNextEpisode2");
            EndEpisode();
        }
    }
    private void JudgeWhetherEnterNextEpisode2()
    {
        CancelInvoke("AddRewardToRobot");
        CancelInvoke("JudgeWhetherEnterNextEpisode");
        CancelInvoke("JudgeWhetherEnterNextEpisode2");
        EndEpisode();
    }
}
