import {call, put, select} from "redux-saga/effects";
import * as companies from "store/actions/companies";
import { companiesSuccessSelector, currentCompaniesPageSelector } from "store/selectors/companies";
import {get} from "services/api";

export function* getCompanies(api) {
	try {
		let currentPage = yield select(currentCompaniesPageSelector);
		currentPage = currentPage || 0;
		yield put(companies.setCurrentPage(currentPage + 1));
		let result = 	yield call(get, `company/getByPage/${api.count}/${currentPage + 1}`);
		if(result.status < 400){
			if(result.data.length) {
				let allCompanies = yield select(companiesSuccessSelector);
				allCompanies = allCompanies || [];
				allCompanies = [...allCompanies, ...result.data];
				yield put(companies.succeededCompanies(allCompanies));
			}
		} else {
			yield put(companies.failedCompanies(result.status))
		}
	} catch(e) {
		yield put(companies.failedCompanies(e.message));
	}
}

export function* getFavoriteCompanies(api) {
	try {
		let result = yield call(get, `company/getFavorites/${api.count}`);
		if (result.status < 400) {
			yield put(companies.succeededFavoriteCompanies(result.data));
		} else {
			yield put(companies.failedFavoriteCompanies(result.status))
		}
	} catch (e) {
		yield put(companies.failedFavoriteCompanies(e.message));
	}
}