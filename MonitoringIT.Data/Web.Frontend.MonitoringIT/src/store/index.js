import { createStore, compose, applyMiddleware } from "redux";
import createSagaMiddleware from "redux-saga";
import rootReducer from "store/reducers";
import rootSaga from "store/sagas";

const configureStore = () => {
	const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;
	const sagaMiddleware = createSagaMiddleware();
	composeEnhancers(applyMiddleware(sagaMiddleware));
	return {
		...createStore(rootReducer, composeEnhancers(applyMiddleware(sagaMiddleware))),
		runSaga: sagaMiddleware.run(rootSaga)
	}
};

export default configureStore;