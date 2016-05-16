using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RadianIBeaconReceiver : MonoBehaviour {
	private Vector2 scrolldistance;
	public  List<Beacon> beacons = new List<Beacon>();
//	public Dictionary<int,Beacon> beaconDict = new Dictionary<int,Beacon>() ; 
//	private bool scanning = true;
	// Use this for initialization


	void OnEnable (){
		iBeaconReceiver.BeaconRangeChangedEvent += OnBeaconRangeChanged;
		iBeaconReceiver.BluetoothStateChangedEvent += OnBluetoothStateChanged;
		iBeaconReceiver.CheckBluetoothLEStatus();
		Debug.Log ("Radian : Listening for beacons");		
	}
	
	void OnDisable() {
		iBeaconReceiver.BeaconRangeChangedEvent -= OnBeaconRangeChanged;
		iBeaconReceiver.BluetoothStateChangedEvent -= OnBluetoothStateChanged;
		Debug.Log ("Radian : Stop Listening for beacons");
	}


	private void OnBluetoothStateChanged(BluetoothLowEnergyState newstate) {
		switch (newstate) {
		case BluetoothLowEnergyState.POWERED_ON:
			iBeaconReceiver.Init();
			Debug.Log ("It is on, go searching");
			break;
		case BluetoothLowEnergyState.POWERED_OFF:
			//iBeaconReceiver.EnableBluetooth();
			Debug.Log ("It is off, switch it on");
	   		break;
		case BluetoothLowEnergyState.UNAUTHORIZED:
			Debug.Log("User doesn't want this app to use Bluetooth, too bad");
			break;
		case BluetoothLowEnergyState.UNSUPPORTED:
			Debug.Log ("This device doesn't support Bluetooth Low Energy, we should inform the user");
			break;
		case BluetoothLowEnergyState.UNKNOWN:
		case BluetoothLowEnergyState.RESETTING:
		default:
			Debug.Log ("Nothing to do at the moment");
			break;
		}
	}


	private void OnBeaconRangeChanged(List<Beacon> _beacons) { // 
		foreach (Beacon b in _beacons) {
			if (b.range  == BeaconRange.UNKNOWN) continue ; 
			if (beacons.Contains(b)) {
					beacons[beacons.IndexOf(b)] = b;
			} else {
				// this beacon was not in the list before
				// this would be the place where the BeaconArrivedEvent would have been spawned in the the earlier versions
				beacons.Add(b);
			}
		}
		foreach (Beacon b in beacons) {
			if (b.lastSeen.AddSeconds(2) < DateTime.Now) {
				// we delete the beacon if it was last seen more than 10 seconds ago
				// this would be the place where the BeaconOutOfRangeEvent would have been spawned in the earlier versions
				b.accuracy = 10f ;
			}
		}

	}



//	void OnGUI() {
//		GUIStyle labelStyle = GUI.skin.GetStyle("Label");
//#if UNITY_ANDROID
//		labelStyle.fontSize = 40;
//#elif UNITY_IOS
//		labelStyle.fontSize = 25;
//#endif
//		float currenty = 10;
//		float labelHeight = labelStyle.CalcHeight(new GUIContent("IBeacons"), Screen.width-20);
//		GUI.Label(new Rect(currenty,10,Screen.width-20,labelHeight),"IBeacons");
//		
//		currenty += labelHeight;
//		scrolldistance = GUI.BeginScrollView(new Rect(10,currenty,Screen.width -20, Screen.height - currenty - 10),scrolldistance,new Rect(0,0,Screen.width - 20,beacons.Count*100));
//		GUILayout.BeginVertical("box",GUILayout.Width(Screen.width-20),GUILayout.Height(50));
//		foreach (Beacon b in beacons) {
////			GUILayout.Label("UUID: "+b.UUID);
//			GUILayout.Label("Id: "+b.major + " ," + b.accuracy);
////			GUILayout.Label("Minor: "+b.minor);
////			GUILayout.Label("Range: "+b.range.ToString());
////			GUILayout.Label("Rssi: "+b.rssi);
//		}
//		GUILayout.EndVertical();
//		GUI.EndScrollView();
//	}
}
