import { call, put } from "redux-saga/effects";
import * as githubProfiles from "store/actions/githubProfiles";
import { get } from "services/api";

export function* getAllProfiles() {
	try {
		let result = 	yield call(get, "github/getAll");
		if(result.status < 400){
			yield put(githubProfiles.succeededAllGithubProfiles(result.data));
		} else {
			yield put(githubProfiles.failedAllGithubProfiles(result.status))
		}
	} catch(e) {
		yield put(githubProfiles.failedAllGithubProfiles(e.message));
	}
}