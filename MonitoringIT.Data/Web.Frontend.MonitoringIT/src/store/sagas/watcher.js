import { takeLatest } from "redux-saga/effects";
import types from "store/types";

import { getAllProfiles } from "./githubProfiles";

export default function* root() {
	yield takeLatest(types.REQUESTED_ALL_GITHUB_PROFILES, getAllProfiles);
}