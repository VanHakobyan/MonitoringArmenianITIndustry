import React from "react";
// @material-ui/core components
import withStyles from "@material-ui/core/styles/withStyles";
import Slide from "@material-ui/core/Slide";
import IconButton from "@material-ui/core/IconButton";
import Dialog from "@material-ui/core/Dialog";
import DialogTitle from "@material-ui/core/DialogTitle";
import DialogContent from "@material-ui/core/DialogContent";
import DialogActions from "@material-ui/core/DialogActions";
// @material-ui/icons
import LibraryBooks from "@material-ui/icons/LibraryBooks";
import Close from "@material-ui/icons/Close";
// core components
import GridContainer from "components/Grid/GridContainer.jsx";
import GridItem from "components/Grid/GridItem.jsx";
import Button from "components/CustomButtons/Button.jsx";
import javascriptStyles from "assets/jss/material-kit-react/views/componentsSections/javascriptStyles.jsx";

function Transition(props) {
	return <Slide direction="down" {...props} />;
}

class Modal extends React.Component {
	constructor(props) {
		super(props);
		this.state = {
			classicModal: false,
			openLeft: false,
			openTop: false,
			openBottom: false,
			openRight: false
		};
	}

	handleClickOpen(modal) {
		let x = [];
		x[modal] = true;
		this.setState(x);
	}

	handleClose(modal) {
		let x = [];
		x[modal] = false;
		this.setState(x);
	}

	render() {
		const {classes} = this.props;
		return (
			<GridContainer>
				<GridItem xs={12} sm={12} md={6}>
					<GridContainer>
						<GridItem xs={12} sm={12} md={6} lg={4}>
							<Button
								onClick={() => this.handleClickOpen("classicModal")}
							>
								Recommend
							</Button>
							<Dialog
								classes={{
									root: classes.center,
									paper: classes.modal
								}}
								open={this.state.classicModal}
								TransitionComponent={Transition}
								keepMounted
								onClose={() => this.handleClose("classicModal")}
								aria-labelledby="classic-modal-slide-title"
								aria-describedby="classic-modal-slide-description"
							>
								<DialogTitle
									id="classic-modal-slide-title"
									disableTypography
									className={classes.modalHeader}
								>
									<IconButton
										className={classes.modalCloseButton}
										key="close"
										aria-label="Close"
										color="inherit"
										onClick={() => this.handleClose("classicModal")}
									>
										<Close className={classes.modalClose}/>
									</IconButton>
									<h4 className={classes.modalTitle}>Modal title</h4>
								</DialogTitle>
								<DialogContent
									id="classic-modal-slide-description"
									className={classes.modalBody}
								>
									<p>
										Far far away, behind the word mountains, far from the
										countries Vokalia and Consonantia, there live the blind
										texts. Separated they live in Bookmarksgrove right at
										the coast of the Semantics, a large language ocean. A
										small river named Duden flows by their place and
										supplies it with the necessary regelialia. It is a
										paradisematic country, in which roasted parts of
										sentences fly into your mouth. Even the all-powerful
										Pointing has no control about the blind texts it is an
										almost unorthographic life One day however a small line
										of blind text by the name of Lorem Ipsum decided to
										leave for the far World of Grammar.
									</p>
								</DialogContent>
								<DialogActions className={classes.modalFooter}>
									<Button
										onClick={() => this.handleClose("classicModal")}
										color="danger"
										simple
									>
										Close
									</Button>
								</DialogActions>
							</Dialog>
						</GridItem>
					</GridContainer>
				</GridItem>
			</GridContainer>
		);
	}
}

export default withStyles(javascriptStyles)(Modal);
