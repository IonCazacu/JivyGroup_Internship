import React from "react";

const useInfiniteScroll = (getUsers: CallableFunction) => {

  const loadMoreRef = React.useRef<HTMLParagraphElement>(null);
  
  React.useEffect(() => {
    
    const handleObserver: IntersectionObserverCallback = (entries: IntersectionObserverEntry[]) => {
      const [target] = entries;
      if (target.isIntersecting) {
        getUsers();
      }
    };

    const option = {
      root: null,
      rootMargin: '0px',
      threshold: 1.0
    };

    const observer: IntersectionObserver = new IntersectionObserver(handleObserver, option);

    let observerRefValue: HTMLParagraphElement;

    if (loadMoreRef.current) {
      observer.observe(loadMoreRef.current);
      observerRefValue = loadMoreRef.current;
    }

    return () => {
      if (observerRefValue) {
        observer.unobserve(observerRefValue);
      }
    };

  }, [getUsers]);

  return loadMoreRef;

}

export default useInfiniteScroll;
