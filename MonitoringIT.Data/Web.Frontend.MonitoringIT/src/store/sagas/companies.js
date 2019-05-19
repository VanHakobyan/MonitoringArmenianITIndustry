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


export function* getCompaniesByPage(api) {
    try {
        let result = 	yield call(get, `company/getByPage/${api.count}/${api.currentPage}`);
        if(result.status < 400){
            yield put(companies.succeededCompaniesByPage(result.data));
        } else {
            yield put(companies.failedCompaniesByPage(result.status))
        }
    } catch(e) {
        yield put(companies.failedCompaniesByPage(e.message));
    }
}