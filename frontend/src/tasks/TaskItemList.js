import { useState, useEffect } from 'react';
import TaskItemListView from './TaskItemListView.js';

// TODO: don't hard-code API domain here
const rootApiUri = 'http://localhost:8000/api/tasks';

export default function TaskItemList() {
  const [taskItems, setTaskItems] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch(rootApiUri);
        if (!response.ok) {
          throw new Error('Error retrieving tasks');
        }
        const allTaskItems = await response.json();
        setTaskItems(allTaskItems);
      } catch (error) {
        setError(error.message);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  const handleTaskItemEdit = async (updatedTaskItem) => {
    try {
      setError(null);

      const response = await fetch(`${rootApiUri}/${updatedTaskItem.id}`, {
        method: 'PUT',
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify(updatedTaskItem)
      });

      if (!response.ok) {
        const valResult = await response.json();
        throw new Error(valResult.message);
      }
      else {
        setTaskItems(currTaskItems => currTaskItems.map(x => {
          if (x.id === updatedTaskItem.id) {
            return {
              ...x,
              description: updatedTaskItem.description,
              isDone: updatedTaskItem.isDone};
          } else {
            return x;
          }
        }));
      }
    } catch (error) {
      console.error("Edit task failed:", error);
      setError(error.message);
    }
  };

  const handleTaskItemDelete = async (id) => {
    try {
      setError(null);

      const response = await fetch(`${rootApiUri}/${id}`, {
        method: "DELETE"
      });

      if (!response.ok) {
        throw new Error("Failed to delete task");
      }
      else {
        setTaskItems(currTaskItems => currTaskItems.filter(x => x.id !== id));
      }
    } catch (error) {
      console.error("Delete task failed:", error);
      setError(error.message);
    }
  };

  const handleAddBtnOnClick = async () => {
    setError(null);
    const description = window.prompt('Task Description:');
    if (description !== null) {
      try {
        setError(null);

        const response = await fetch(rootApiUri, {
          method: 'POST',
          headers: {'Content-Type': 'application/json'},
          body: JSON.stringify({description: description, isDone: false})
        });

        if (!response.ok) {
          const valResult = await response.json();
          throw new Error(valResult.message);
        }
        else {
          const newTaskItem = await response.json();
          setTaskItems(currTaskItems => [...currTaskItems, newTaskItem]);
        }
      } catch (error) {
        console.error('Failed to create task:', error);
        setError(error.message);
      }
    }
  }

  return <TaskItemListView
            taskItems={taskItems}
            error={error}
            loading={loading}
            handleAddBtnOnClick={handleAddBtnOnClick}
            handleTaskItemEdit={handleTaskItemEdit}
            handleTaskItemDelete={handleTaskItemDelete}/>
}
