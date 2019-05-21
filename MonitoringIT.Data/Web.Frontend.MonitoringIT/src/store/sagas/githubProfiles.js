import { call, put, select } from "redux-saga/effects";
import * as githubProfiles from "store/actions/githubProfiles";
import { currentGithubPageSelector, allProfilesSuccessSelector } from "store/selectors/githubProfiles";
import { get } from "services/api";

export function* getAllGithubProfiles(api) {
	try {
		let currentPage = yield select(currentGithubPageSelector);
		currentPage = currentPage || 0;
		yield put(githubProfiles.setCurrentPage(currentPage + 1));
		let result = 	yield call(get, `github/getByPage/${api.count}/${currentPage + 1}`);
		if(result.status < 400){
			if(result.data.length) {
				let allGithubProfiles = yield select(allProfilesSuccessSelector);
				allGithubProfiles = allGithubProfiles || [];
				allGithubProfiles = [...allGithubProfiles, ...result.data];
				yield put(githubProfiles.succeededAllGithubProfiles(allGithubProfiles));
			}
		} else {
			yield put(githubProfiles.failedAllGithubProfiles(result.status))
		}
	} catch(e) {
		yield put(githubProfiles.failedAllGithubProfiles(e.message));
	}
}
export function* getFavoriteGithubProfiles(api) {
	try {
		let result = 	yield call(get, `github/getFavorites/${api.count}`);
		if(result.status < 400){
			yield put(githubProfiles.succeededFavoriteGithubProfiles(result.data));
		} else {
			yield put(githubProfiles.failedFavoriteGithubProfiles(result.status))
		}
	} catch(e) {
		yield put(githubProfiles.failedFavoriteGithubProfiles(e.message));
	}
}