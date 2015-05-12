using System;

namespace Communication
{

	enum RequestType {
		get_new, // for get new report and stuffs from server.
		new_report, // for sending new report
		delete_report, // for deleting a report
		mark_report, // for marking a report
		read_report // for changing report read flag 
	}
}