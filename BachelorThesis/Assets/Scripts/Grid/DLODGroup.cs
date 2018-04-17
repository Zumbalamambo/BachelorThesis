﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DLODGroup : MonoBehaviour {
    public List<GameObject> dlods;
    private int active_version = -1;

    public int num_dlod_versions() {
        return dlods.Count;
    }

    public MeshFilter get_active_dlod_mesh_filter() {
        if (active_version == -1) {
            return null;
        }

        return dlods[active_version].GetComponent<MeshFilter>();
    }

    public bool try_to_lower() {
        if (active_version == -1) {
            return false;
        }

        int version = active_version + 1;

        if (version > dlods.Count - 1) {
            return false;
        }

        if (version == dlods.Count - 1) {
            dlods[active_version].SetActive(false);
            active_version = -1;
            return true;
        }

        dlods[active_version].SetActive(false);
        dlods[version].SetActive(true);
        active_version = version;

        return true;
    }

    public bool try_to_higher() {
        int version = active_version;
        if (version == -1) {
            version = dlods.Count - 1;
            dlods[version].SetActive(true);
            active_version = version;
            return true;
        }

        version -= 1;

        if (version < 0) {
            return false;
        }

        dlods[active_version].SetActive(false);
        dlods[version].SetActive(true);
        active_version = version;
        return true;
    }

    public void set_to_original() {
        if (dlods.Count == 0) {
            return;
        }

        if (active_version == 0) {
            return;
        }

        if (active_version != -1) {
            dlods[active_version].SetActive(false);
        }

        dlods[0].SetActive(true);
        active_version = 0;
    }

    public void activate(int version) {
        if (dlods.Count == 0) {
            return;
        }

        if (active_version != -1) {
            dlods[active_version].SetActive(false);
        }

        dlods[version].SetActive(true);
        active_version = version;
    }
}
