import { call, put } from "redux-saga/effects";
import * as jobs from "store/actions/jobs";
import { get } from "services/api";



export function* getJobsByPage(api) {
    try {
        let result = 	yield call(get, `job/getByPage/${api.count}/${api.currentPage}`);
        if(result.status < 400){
            yield put(jobs.succeededJobsByPage(result.data));
        } else {
            yield put(jobs.succeededJobsByPage(result.status))
        }
    } catch(e) {
        yield put(jobs.failedJobsByPage(e.message));
    }
}