import { fork } from "redux-saga/effects";
import watcher from "./watcher";

export default function* startWatcher() {
	yield fork(watcher);
}