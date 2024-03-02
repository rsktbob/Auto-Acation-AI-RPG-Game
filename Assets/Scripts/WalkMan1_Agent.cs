using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using System;
using Unity.Mathematics;

public class WalkMan1_Agent : Agent
{
    [SerializeField] private GameObject robotPrefab;
    [SerializeField] private float InitPositionMinY;
    [SerializeField] private float InitPositionMaxY;
    [SerializeField] private float rewardUpperLimit;
    private Walkman robot;

    // reset the agent or remove it from the environment
    public override void OnEpisodeBegin()
    {
        InvokeRepeating("AddRewardToRobot", 1f, 1f); // add reward every 1 sec
        InvokeRepeating("JudgeWhetherEnterNextEpisode", 7f, 7f); // judge next episode every 10 sec
        CreateNewRobot();
    }

    // to specify agent behavior at every step, based on the provided action
    public override void OnActionReceived(ActionBuffers actions)
    {

        /* Left Leg, Foot, Toe */
        Vector3 targetLeftUpLegAngularVelocity = new Vector3(
            actions.ContinuousActions[0],
            actions.ContinuousActions[1],
            actions.ContinuousActions[2]
            ) * 5; //50;
        Vector3 targetLeftLegAngularVelocity = new Vector3(
            actions.ContinuousActions[3],
            actions.ContinuousActions[4],
            actions.ContinuousActions[5]
            ) * 5; //50;
        Vector3 targetLeftFootAngularVelocity = new Vector3(
            actions.ContinuousActions[6],
            actions.ContinuousActions[7],
            actions.ContinuousActions[8]
            ) * 6; //100;
        Vector3 targetLeftToeAngularVelocity = new Vector3(
            actions.ContinuousActions[9],
            actions.ContinuousActions[10],
            actions.ContinuousActions[11]
            ) * 5; //50;

        /* Right leg, Foot, Toe */
        Vector3 targetRightUpLegAngularVelocity = new Vector3(
            actions.ContinuousActions[12],
            actions.ContinuousActions[13],
            actions.ContinuousActions[14]
            ) * 5; //50;
        Vector3 targetRightLegAngularVelocity = new Vector3(
            actions.ContinuousActions[15],
            actions.ContinuousActions[16],
            actions.ContinuousActions[17]
            ) * 5; //50;
        Vector3 targetRightFootAngularVelocity = new Vector3(
            actions.ContinuousActions[18],
            actions.ContinuousActions[19],
            actions.ContinuousActions[20]
            ) * 6; //100;
        Vector3 targetRightToeAngularVelocity = new Vector3(
            actions.ContinuousActions[21],
            actions.ContinuousActions[22],
            actions.ContinuousActions[23]
            ) * 5; //50;

        /* Left shoulder, arm, hand */
        Vector3 targetLeftShoulderAngularVelocity = new Vector3(
            actions.ContinuousActions[24],
            actions.ContinuousActions[25],
            actions.ContinuousActions[26]
            ) * 4; //30;
        Vector3 targetLeftArmAngularVelocity = new Vector3(
            actions.ContinuousActions[27],
            actions.ContinuousActions[28],
            actions.ContinuousActions[29]
            ) * 3; //22;
        Vector3 targetLeftHandAngularVelocity = new Vector3(
            actions.ContinuousActions[30],
            actions.ContinuousActions[31],
            actions.ContinuousActions[32]
            ) * 3; //22;

        /* Right shoulder, arm, hand */
        Vector3 targetRightShoulderAngularVelocity = new Vector3(
            actions.ContinuousActions[33],
            actions.ContinuousActions[34],
            actions.ContinuousActions[35]
            ) * 4; //30;
        Vector3 targetRightArmAngularVelocity = new Vector3(
            actions.ContinuousActions[36],
            actions.ContinuousActions[37],
            actions.ContinuousActions[38]
            ) * 3; //22;
        Vector3 targetRightHandAngularVelocity = new Vector3(
            actions.ContinuousActions[39],
            actions.ContinuousActions[40],
            actions.ContinuousActions[41]
            ) * 3; //22;

        /* Hip */
        Vector3 targetHipAngularVelocity = new Vector3(
            actions.ContinuousActions[42],
            actions.ContinuousActions[43],
            actions.ContinuousActions[44]
            ) * 7; //160;

        /* Chest and Waist and Head */
        Vector3 targetWaistAngularVelocity = new Vector3(
            actions.ContinuousActions[45],
            actions.ContinuousActions[46],
            actions.ContinuousActions[47]
            ) * 5; //45;

        Vector3 targetHeadAngularVelocity = new Vector3(
            actions.ContinuousActions[48],
            actions.ContinuousActions[49],
            actions.ContinuousActions[50]
            ) * 2; //7;
        Debug.Log(actions.ContinuousActions[48]);

        // output Left Leg, Foot, Toe
        robot.LeftLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity = new Vector3(Mathf.Clamp(robot.LeftLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity.x + targetLeftLegAngularVelocity.x, -50f, 50f), Mathf.Clamp(robot.LeftLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity.y + targetLeftLegAngularVelocity.y, -50f, 50f), Mathf.Clamp(robot.LeftLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity.z + targetLeftLegAngularVelocity.z, -50f, 50f));
        robot.LeftUpLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity = new Vector3(Mathf.Clamp(robot.LeftUpLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity.x + targetLeftUpLegAngularVelocity.x, -50f, 50f), Mathf.Clamp(robot.LeftUpLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity.y + targetLeftUpLegAngularVelocity.y, -50f, 50f), Mathf.Clamp(robot.LeftUpLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity.z + targetLeftUpLegAngularVelocity.z, -50f, 50f)); ;
        robot.LeftFoot.GetComponent<ConfigurableJoint>().targetAngularVelocity = new Vector3(Mathf.Clamp(robot.LeftFoot.GetComponent<ConfigurableJoint>().targetAngularVelocity.x + targetLeftFootAngularVelocity.x, -100f, 100f), Mathf.Clamp(robot.LeftFoot.GetComponent<ConfigurableJoint>().targetAngularVelocity.y + targetLeftFootAngularVelocity.y, -50f, 50f), Mathf.Clamp(robot.LeftFoot.GetComponent<ConfigurableJoint>().targetAngularVelocity.z + targetLeftFootAngularVelocity.z, -50f, 50f)); ;
        robot.LeftToe.GetComponent<ConfigurableJoint>().targetAngularVelocity = new Vector3(Mathf.Clamp(robot.LeftToe.GetComponent<ConfigurableJoint>().targetAngularVelocity.x + targetLeftToeAngularVelocity.x, -50f, 50f), Mathf.Clamp(robot.LeftToe.GetComponent<ConfigurableJoint>().targetAngularVelocity.y + targetLeftToeAngularVelocity.y, -50f, 50f), Mathf.Clamp(robot.LeftToe.GetComponent<ConfigurableJoint>().targetAngularVelocity.z + targetLeftToeAngularVelocity.z, -50f, 50f)); ;

        // output right Leg, Foot, Toe
        robot.RightLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity = new Vector3(Mathf.Clamp(robot.RightLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity.x + targetRightLegAngularVelocity.x, -50f, 50f), Mathf.Clamp(robot.RightLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity.y + targetRightLegAngularVelocity.y, -50f, 50f), Mathf.Clamp(robot.RightLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity.z + targetRightLegAngularVelocity.z, -50f, 50f)); ;
        robot.RightUpLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity = new Vector3(Mathf.Clamp(robot.RightUpLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity.x + targetRightUpLegAngularVelocity.x, -50f, 50f), Mathf.Clamp(robot.RightUpLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity.y + targetRightUpLegAngularVelocity.y, -50f, 50f), Mathf.Clamp(robot.RightUpLeg.GetComponent<ConfigurableJoint>().targetAngularVelocity.z + targetRightUpLegAngularVelocity.z, -50f, 50f)); ;
        robot.RightFoot.GetComponent<ConfigurableJoint>().targetAngularVelocity = new Vector3(Mathf.Clamp(robot.RightFoot.GetComponent<ConfigurableJoint>().targetAngularVelocity.x + targetRightFootAngularVelocity.x, -100f, 100f), Mathf.Clamp(robot.RightFoot.GetComponent<ConfigurableJoint>().targetAngularVelocity.y + targetRightFootAngularVelocity.y, -50f, 50f), Mathf.Clamp(robot.RightFoot.GetComponent<ConfigurableJoint>().targetAngularVelocity.z + targetRightFootAngularVelocity.z, -50f, 50f)); ;
        robot.RightToe.GetComponent<ConfigurableJoint>().targetAngularVelocity = new Vector3(Mathf.Clamp(robot.RightToe.GetComponent<ConfigurableJoint>().targetAngularVelocity.x + targetRightToeAngularVelocity.x, -50f, 50f), Mathf.Clamp(robot.RightToe.GetComponent<ConfigurableJoint>().targetAngularVelocity.y + targetRightToeAngularVelocity.y, -50f, 50f), Mathf.Clamp(robot.RightToe.GetComponent<ConfigurableJoint>().targetAngularVelocity.z + targetRightToeAngularVelocity.z, -50f, 50f)); ;

        // output Left shoulder, arm, hand
        robot.LeftShoulder.GetComponent<ConfigurableJoint>().targetAngularVelocity = new Vector3(Mathf.Clamp(robot.LeftShoulder.GetComponent<ConfigurableJoint>().targetAngularVelocity.x + targetLeftShoulderAngularVelocity.x, -30f, 30f), Mathf.Clamp(robot.LeftShoulder.GetComponent<ConfigurableJoint>().targetAngularVelocity.y + targetLeftShoulderAngularVelocity.y, -30f, 30f), Mathf.Clamp(robot.LeftShoulder.GetComponent<ConfigurableJoint>().targetAngularVelocity.z + targetLeftShoulderAngularVelocity.z, -30f, 30f)); ;
        robot.LeftArm.GetComponent<ConfigurableJoint>().targetAngularVelocity = new Vector3(Mathf.Clamp(robot.LeftArm.GetComponent<ConfigurableJoint>().targetAngularVelocity.x + targetLeftArmAngularVelocity.x, -22f, 22f), Mathf.Clamp(robot.LeftArm.GetComponent<ConfigurableJoint>().targetAngularVelocity.y + targetLeftArmAngularVelocity.y, -22f, 22f), Mathf.Clamp(robot.LeftArm.GetComponent<ConfigurableJoint>().targetAngularVelocity.z + targetLeftArmAngularVelocity.z, -22f, 22f)); ;
        robot.LeftHand.GetComponent<ConfigurableJoint>().targetAngularVelocity = new Vector3(Mathf.Clamp(robot.LeftHand.GetComponent<ConfigurableJoint>().targetAngularVelocity.x + targetLeftHandAngularVelocity.x, -22f, 22f), Mathf.Clamp(robot.LeftHand.GetComponent<ConfigurableJoint>().targetAngularVelocity.y + targetLeftHandAngularVelocity.y, -22f, 22f), Mathf.Clamp(robot.LeftHand.GetComponent<ConfigurableJoint>().targetAngularVelocity.z + targetLeftHandAngularVelocity.z, -22f, 22f)); ;

        // output Right shoulder, arm, hand
        robot.RightShoulder.GetComponent<ConfigurableJoint>().targetAngularVelocity = new Vector3(Mathf.Clamp(robot.RightShoulder.GetComponent<ConfigurableJoint>().targetAngularVelocity.x + targetRightShoulderAngularVelocity.x, -30f, 30f), Mathf.Clamp(robot.RightShoulder.GetComponent<ConfigurableJoint>().targetAngularVelocity.y + targetRightShoulderAngularVelocity.y, -30f, 30f), Mathf.Clamp(robot.RightShoulder.GetComponent<ConfigurableJoint>().targetAngularVelocity.z + targetRightShoulderAngularVelocity.z, -30f, 30f)); ;
        robot.RightArm.GetComponent<ConfigurableJoint>().targetAngularVelocity = new Vector3(Mathf.Clamp(robot.RightArm.GetComponent<ConfigurableJoint>().targetAngularVelocity.x + targetRightArmAngularVelocity.x, -22f, 22f), Mathf.Clamp(robot.RightArm.GetComponent<ConfigurableJoint>().targetAngularVelocity.y + targetRightArmAngularVelocity.y, -22f, 22f), Mathf.Clamp(robot.RightArm.GetComponent<ConfigurableJoint>().targetAngularVelocity.z + targetRightArmAngularVelocity.z, -22f, 22f)); ;
        robot.RightHand.GetComponent<ConfigurableJoint>().targetAngularVelocity = new Vector3(Mathf.Clamp(robot.RightHand.GetComponent<ConfigurableJoint>().targetAngularVelocity.x + targetRightHandAngularVelocity.x, -22f, 22f), Mathf.Clamp(robot.RightHand.GetComponent<ConfigurableJoint>().targetAngularVelocity.y + targetRightHandAngularVelocity.y, -22f, 22f), Mathf.Clamp(robot.RightHand.GetComponent<ConfigurableJoint>().targetAngularVelocity.z + targetRightHandAngularVelocity.z, -22f, 22f)); ;

        // output Hip
        robot.Hip.GetComponent<ConfigurableJoint>().targetAngularVelocity = new Vector3(Mathf.Clamp(robot.Hip.GetComponent<ConfigurableJoint>().targetAngularVelocity.x + targetHipAngularVelocity.x, -160f, 160f), Mathf.Clamp(robot.Hip.GetComponent<ConfigurableJoint>().targetAngularVelocity.y + targetHipAngularVelocity.y/3, -15f, 15f), Mathf.Clamp(robot.Hip.GetComponent<ConfigurableJoint>().targetAngularVelocity.z + targetHipAngularVelocity.z / 3, -15f, 15f)); ;

        // output Chest, Waist, Head
        robot.Waist.GetComponent<ConfigurableJoint>().targetAngularVelocity = new Vector3(Mathf.Clamp(robot.Waist.GetComponent<ConfigurableJoint>().targetAngularVelocity.x + targetWaistAngularVelocity.x, -45f, 45f), Mathf.Clamp(robot.Waist.GetComponent<ConfigurableJoint>().targetAngularVelocity.y + targetWaistAngularVelocity.y, -45f, 45f), Mathf.Clamp(robot.Waist.GetComponent<ConfigurableJoint>().targetAngularVelocity.z + targetWaistAngularVelocity.z, -45f, 45f)); ;
        robot.Head.GetComponent<ConfigurableJoint>().targetAngularVelocity = new Vector3(Mathf.Clamp(robot.Head.GetComponent<ConfigurableJoint>().targetAngularVelocity.x + targetHeadAngularVelocity.x, -7f, 7f), Mathf.Clamp(robot.Head.GetComponent<ConfigurableJoint>().targetAngularVelocity.y + targetHeadAngularVelocity.y, -7f, 7f), Mathf.Clamp(robot.Head.GetComponent<ConfigurableJoint>().targetAngularVelocity.z + targetHeadAngularVelocity.z, -7f, 7f)); ;


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

        /* Left Toe */
        sensor.AddObservation(RelativeBodyPosition(robot.LeftToe.transform.position)); // position
        sensor.AddObservation(robot.LeftToe.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.LeftToe.GetComponent<Rigidbody>().angularVelocity); // angular velocity

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

        /* Right Toe */
        sensor.AddObservation(RelativeBodyPosition(robot.RightToe.transform.position)); // position
        sensor.AddObservation(robot.RightToe.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.RightToe.GetComponent<Rigidbody>().angularVelocity); // angular velocity

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
        sensor.AddObservation(robot.Hip.transform.localPosition); // position
        sensor.AddObservation(robot.Hip.GetComponent<Rigidbody>().velocity); // velocity
        sensor.AddObservation(robot.Hip.GetComponent<Rigidbody>().angularVelocity); // angular velocity
        
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

        //is touch floor
        sensor.AddObservation(robot.LeftUpLeg.GetComponent<bodyCollision>().isTouchFloor);
        sensor.AddObservation(robot.LeftLeg.GetComponent<bodyCollision>().isTouchFloor);
        sensor.AddObservation(robot.LeftFoot.GetComponent<bodyCollision>().isTouchFloor);
        sensor.AddObservation(robot.LeftToe.GetComponent<bodyCollision>().isTouchFloor);
        sensor.AddObservation(robot.RightUpLeg.GetComponent<bodyCollision>().isTouchFloor);
        sensor.AddObservation(robot.RightLeg.GetComponent<bodyCollision>().isTouchFloor);
        sensor.AddObservation(robot.RightFoot.GetComponent<bodyCollision>().isTouchFloor);
        sensor.AddObservation(robot.RightToe.GetComponent<bodyCollision>().isTouchFloor);
        sensor.AddObservation(robot.LeftShoulder.GetComponent<bodyCollision>().isTouchFloor);
        sensor.AddObservation(robot.RightArm.GetComponent<bodyCollision>().isTouchFloor);
        sensor.AddObservation(robot.RightHand.GetComponent<bodyCollision>().isTouchFloor);
        sensor.AddObservation(robot.Hip.GetComponent<bodyCollision>().isTouchFloor);
        sensor.AddObservation(robot.Chest.GetComponent<bodyCollision>().isTouchFloor);
        sensor.AddObservation(robot.Waist.GetComponent<bodyCollision>().isTouchFloor);
        sensor.AddObservation(robot.Head.GetComponent<bodyCollision>().isTouchFloor);
    }

    //Add reward to robot
    private void AddRewardToRobot() 
    {
        float LeftFootToHeadDistance = robot.Head.transform.localPosition.y - robot.LeftFoot.transform.localPosition.y;
        float RightFootToHeadDistance = robot.Head.transform.localPosition.y - robot.RightFoot.transform.localPosition.y;
        float LeftFootToHipDistance = robot.Hip.transform.localPosition.y - robot.RightFoot.transform.localPosition.y;
        float RightFootToHipDistance = robot.Hip.transform.localPosition.y - robot.RightFoot.transform.localPosition.y;
        AddReward((LeftFootToHeadDistance - 1)*2);
        AddReward((RightFootToHeadDistance - 1)*2);
        AddReward((LeftFootToHipDistance - 1) * 2);
        AddReward((RightFootToHipDistance - 1) * 2);

        float eulerChestX = robot.Chest.transform.eulerAngles.x < 180 ? robot.Chest.transform.eulerAngles.x : 360 - robot.Chest.transform.eulerAngles.x;
        float eulerChestZ = robot.Chest.transform.eulerAngles.z < 180 ? robot.Chest.transform.eulerAngles.z : 360 - robot.Chest.transform.eulerAngles.z;
        float eulerHipX = robot.Hip.transform.eulerAngles.x < 180 ? robot.Hip.transform.eulerAngles.x : 360 - robot.Hip.transform.eulerAngles.x;
        float eulerHipZ = robot.Hip.transform.eulerAngles.z < 180 ? robot.Hip.transform.eulerAngles.z : 360 - robot.Hip.transform.eulerAngles.z;
        float eulerHeadX = robot.Head.transform.eulerAngles.x < 180 ? robot.Head.transform.eulerAngles.x : 360 - robot.Head.transform.eulerAngles.x;
        float eulerHeadZ = robot.Head.transform.eulerAngles.z < 180 ? robot.Head.transform.eulerAngles.z : 360 - robot.Head.transform.eulerAngles.z;
        float eulerLeftFootX = robot.LeftFoot.transform.eulerAngles.x < 180 ? robot.LeftFoot.transform.eulerAngles.x : 360 - robot.LeftFoot.transform.eulerAngles.x;
        float eulerLeftFootZ = robot.LeftFoot.transform.eulerAngles.z < 180 ? robot.LeftFoot.transform.eulerAngles.z : 360 - robot.LeftFoot.transform.eulerAngles.z;
        float eulerRightFootX = robot.RightFoot.transform.eulerAngles.x < 180 ? robot.RightFoot.transform.eulerAngles.x : 360 - robot.RightFoot.transform.eulerAngles.x;
        float eulerRightFootZ = robot.RightFoot.transform.eulerAngles.z < 180 ? robot.RightFoot.transform.eulerAngles.z : 360 - robot.RightFoot.transform.eulerAngles.z;
        AddReward((90 - eulerChestX- eulerChestZ) / 80);
        AddReward((90 - eulerHipX - eulerHipZ) / 80);
        AddReward((90 - eulerHeadX - eulerHeadZ) / 160);
        AddReward((90 - eulerLeftFootX - eulerLeftFootZ) / 80);
        AddReward((90 - eulerRightFootX - eulerRightFootZ) / 80);

        AddReward(robot.LeftFoot.GetComponent<bodyCollision>().isTouchFloor ? 1 : -1);
        AddReward(robot.LeftToe.GetComponent<bodyCollision>().isTouchFloor ? 1 : -1);
        AddReward(robot.RightFoot.GetComponent<bodyCollision>().isTouchFloor ? 1 : -1);
        AddReward(robot.RightToe.GetComponent<bodyCollision>().isTouchFloor ? 1 : -1);

        AddReward(-math.abs(robot.Hip.transform.position.x));
        AddReward(-math.abs(robot.Hip.transform.position.z));
        // Height reward
        //float heightReward = robot.Head.transform.localPosition.y - rewardUpperLimit;
        //heightReward = heightReward > 0 ? (float)Math.Pow(20, heightReward) + heightReward : heightReward;
        //AddReward(heightReward);
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
        robot = Instantiate(robotPrefab, new Vector3(transform.position.x, transform.position.y+ 0.6f, transform.position.z), new Quaternion(0,0,0,0)).GetComponent<Walkman>();
        robot.transform.parent = transform;
        
    }

    private Vector3 RelativeBodyPosition(Vector3 position)
    {
        return position - robot.Hip.transform.position;
    }

    // Judge whether enter next episode
    private void JudgeWhetherEnterNextEpisode()
    {
        float actualPositionY = robot.Head.transform.localPosition.y;
        if (actualPositionY < rewardUpperLimit)
        {
            CancelInvoke("AddRewardToRobot");
            CancelInvoke("JudgeWhetherEnterNextEpisode");
            EndEpisode();
        }
    }
}
