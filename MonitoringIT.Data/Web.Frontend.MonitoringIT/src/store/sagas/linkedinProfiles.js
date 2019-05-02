import { call, put } from "redux-saga/effects";
import * as linkedinProfiles from "store/actions/linkedinProfiles";
import { get } from "services/api";
//
// export function* getAllProfiles() {
//     try {
//         let result = 	yield call(get, "linkedin/getAll");
//         if(result.status < 400){
//             yield put(linkedinProfiles.succeededAllGithubProfiles(result.data));
//         } else {
//             yield put(linkedinProfiles.failedAllGithubProfiles(result.status))
//         }
//     } catch(e) {
//         yield put(linkedinProfiles.failedAllGithubProfiles(e.message));
//     }
// }
export function* getFavoriteProfiles(api) {
    try {
        let result = 	yield call(get, `github/getFavorites/${api.count}`);
        if(result.status < 400){
            yield put(linkedinProfiles.succeededFavoriteLinkedinProfiles(result.data));
        } else {
            yield put(linkedinProfiles.failedFavoriteLinkedinProfiles(result.status))
        }
    } catch(e) {
        yield put(linkedinProfiles.failedFavoriteLinkedinProfiles(e.message));
    }
}