﻿select date_rec,count(*),sum(weight) from tbl_purchases group by date_rec;