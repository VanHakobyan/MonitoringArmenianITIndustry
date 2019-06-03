import {call, put, select} from "redux-saga/effects";
import * as linkedinProfiles from "store/actions/linkedinProfiles";
import {currentLinkedinPageSelector, linkedinProfilesSuccessSelector} from "store/selectors/linkedinProfiles";
import {get} from "services/api";


export function* getAllLinkedinProfiles(api) {
	try {
		let currentPage = yield select(currentLinkedinPageSelector);
		currentPage = currentPage || 0;
		yield put(linkedinProfiles.setCurrentPage(currentPage + 1));
		let result = 	yield call(get, `linkedin/getByPage/${api.count}/${currentPage + 1}`);
		if(result.status < 400){
			if(result.data.length) {
				let allLinkedinProfiles = yield select(linkedinProfilesSuccessSelector);
				allLinkedinProfiles = allLinkedinProfiles || [];
				allLinkedinProfiles = [...allLinkedinProfiles, ...result.data];
				yield put(linkedinProfiles.succeededAllLinkedinProfiles(allLinkedinProfiles));
			}
		} else {
			yield put(linkedinProfiles.failedAllLinkedinProfiles(result.status))
		}
	} catch(e) {
		yield put(linkedinProfiles.failedAllLinkedinProfiles(e.message));
	}
}

export function* getFavoriteLinkedinProfiles(api) {
	try {
		let result = yield call(get, `linkedin/getFavorites/${api.count}`);
		if (result.status < 400) {
			yield put(linkedinProfiles.succeededFavoriteLinkedinProfiles(result.data));
		} else {
			yield put(linkedinProfiles.failedFavoriteLinkedinProfiles(result.status))
		}
	} catch (e) {
		yield put(linkedinProfiles.failedFavoriteLinkedinProfiles(e.message));
	}
}
