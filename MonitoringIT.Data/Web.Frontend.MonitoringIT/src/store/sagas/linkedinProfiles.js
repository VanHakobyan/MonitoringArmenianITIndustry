import { call, put } from "redux-saga/effects";
import * as linkedinProfiles from "store/actions/linkedinProfiles";
import { get } from "services/api";

export function* getFavoriteLinkedinProfiles(api) {
    try {
        let result = 	yield call(get, `linkedin/getFavorites/${api.count}`);
        if(result.status < 400){
            yield put(linkedinProfiles.succeededFavoriteLinkedinProfiles(result.data));
        } else {
            yield put(linkedinProfiles.failedFavoriteLinkedinProfiles(result.status))
        }
    } catch(e) {
        yield put(linkedinProfiles.failedFavoriteLinkedinProfiles(e.message));
    }
}

export function* getLinkedinProfilesByPage(api) {
    try {
        let result = 	yield call(get, `linkedin/getByPage/${api.count}/${api.currentPage}`);
        if(result.status < 400){
            yield put(linkedinProfiles.succeededLinkedinProfilesByPage(result.data));
        } else {
            yield put(linkedinProfiles.failedLinkedinProfilesByPage(result.status))
        }
    } catch(e) {
        yield put(linkedinProfiles.failedLinkedinProfilesByPage(e.message));
    }
}