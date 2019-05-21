import {call, put, select} from "redux-saga/effects";
import * as jobs from "store/actions/jobs";
import {jobsSuccessSelector, currentJobsPageSelector} from "store/selectors/jobs";
import {get} from "services/api";

export function* getJobs(api) {
	try {
		let currentPage = yield select(currentJobsPageSelector);
		currentPage = currentPage || 0;
		yield put(jobs.setCurrentPage(currentPage + 1));
		let result = 	yield call(get, `job/getByPage/${api.count}/${currentPage + 1}`);
		if(result.status < 400){
			if(result.data.length) {
				let allJobs = yield select(jobsSuccessSelector);
				allJobs = allJobs || [];
				allJobs = [...allJobs, ...result.data];
				yield put(jobs.succeededJobs(allJobs));
			}
		} else {
			yield put(jobs.failedJobs(result.status))
		}
	} catch(e) {
		yield put(jobs.failedJobs(e.message));
	}
}