import { call, put } from "redux-saga/effects";
import * as companies from "store/actions/companies";
import { get } from "services/api";

export function* getFavoriteCompanies(api) {
    try {
        let result = 	yield call(get, `company/getFavorites/${api.count}`);
        if(result.status < 400){
            yield put(companies.succeededFavoriteCompanies(result.data));
        } else {
            yield put(companies.failedFavoriteCompanies(result.status))
        }
    } catch(e) {
        yield put(companies.failedFavoriteCompanies(e.message));
    }
}