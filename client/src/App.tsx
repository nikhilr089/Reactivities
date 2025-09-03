import { List, ListItem, ListItemText, Typography } from "@mui/material";
import axios from "axios";
import { useEffect, useState } from "react"

function App() {
  const [activities, setActivities] = useState<Activity[]>([]);

  useEffect(() => {
    // fetch('https://localhost:5001/api/activities')
    //   .then(resp => resp.json())
    //   .then(data => setActivities(data))

    axios.get<Activity[]>('https://localhost:5001/api/activities').
      then(x => setActivities(x.data))
    return () => { }
  }, []);


  return (
    // basically this (<>) means shorthand for Fragment a react term
    <>
      <Typography variant="h3">Reactivities</Typography>
      <List>
        {activities.map((activity) => (
          <ListItem key={activity.id}>
            <ListItemText>{activity.title
            }</ListItemText>
          </ListItem>
        ))}
      </List>
    </>
  )
}

export default App
