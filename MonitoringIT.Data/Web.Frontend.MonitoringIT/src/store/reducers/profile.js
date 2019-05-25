import types from "store/types";

export default function reduce(state = {}, action) {
	switch (action.type) {
		case types.REQUESTED_PROFILE:
			return {
				...state,
				currentProfileLoading: true,
				currentProfileData: undefined,
				currentProfileFailed: undefined
			};
		case types.SUCCEEDED_PROFILE:
			return {
				...state,
				currentProfileLoading: false,
				currentProfileData: action.profile,
				currentProfileFailed: undefined
			};
		case types.FAILED_PROFILE:
			return {
				...state,
				currentProfileLoading: false,
				currentProfileData: undefined,
				currentProfileFailed: action.error
			};
		case types.REQUEST_CROSS_PROFILE:
			return {
				...state,
				crossProfileLoading: true,
				crossProfileData: undefined,
				crossProfileFailed: undefined
			};
		case types.SUCCEEDED_CROSS_PROFILE:
			return {
				...state,
				crossProfileLoading: false,
				crossProfileData: action.profiles,
				crossProfileFailed: undefined
			};
		case types.FAILED_CROSS_PROFILE:
			return {
				...state,
				crossProfileLoading: false,
				crossProfileData: undefined,
				crossProfileFailed: action.error
			};
		default:
			return state;
	}
}